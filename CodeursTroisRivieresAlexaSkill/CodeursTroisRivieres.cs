using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using CellNinja.Localization;
using CellNinja.Localization.Resources;
using CodeursTroisRivieresAlexaSkill.RequestHandlers;
using CodeursTroisRivieresAlexaSkill.SkillHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill
{
    public static class CodeursTroisRivieres
    {
        static CodeursTroisRivieres()
        {
            DependencyInjection.RegisterSingleton<ITranslateResource, TranslateResource>();
            DependencyInjection.Verify();
        }

        [FunctionName(nameof(CodeursTroisRivieres))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request,
            ILogger log)
        {
            string locale = Translate.DefaultLocale;

            try
            {
                SkillRequest skillRequest = await GetSkillRequestFromRequest(request);
                locale = skillRequest.Request.Locale;

                bool isValid = await RequestHandlerHelper.ValidateRequest(request, log, skillRequest);
                if (!isValid)
                {
                    return new BadRequestResult();
                }

                ISkillHandler skillHandler = new SkillHandler(skillRequest.Request);
                return await skillHandler.GetResultAsync();
            }
            catch (System.Exception e)
            {
                log.LogError(e.Message);

                string speechText = Translate.Get(nameof(Translations.Error_CanYouRepeat), locale);

                SkillResponse response = ResponseBuilder.Ask(speechText, RequestHandlerHelper.GetDefaultReprompt(locale));
                return new OkObjectResult(response);
            }
        }

        private static async Task<SkillRequest> GetSkillRequestFromRequest(HttpRequest req)
        {
            string json = await req.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SkillRequest>(json);
        }
    }
}
