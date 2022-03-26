using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using CodeursTroisRivieresAlexaSkill.RequestHandlers;
using CodeursTroisRivieresAlexaSkill.SkillHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace CodeursTroisRivieresAlexaSkill
{
    public static class CodeursTroisRivieres
    {
        [FunctionName(nameof(CodeursTroisRivieres))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request,
            ILogger log)
        {
            try
            {
                SkillRequest skillRequest = await GetSkillRequestFromRequest(request);

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

                string speechText = "Pouvez-vous répéter, j'ai mal compris.";

                SkillResponse response = ResponseBuilder.Ask(speechText, RequestHandlerHelper.GetDefaultReprompt());
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
