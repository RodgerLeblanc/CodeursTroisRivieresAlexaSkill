using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CellNinja.Localization.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public class LaunchRequestHandler : BaseRequestHandler<LaunchRequest>
    {
        public LaunchRequestHandler(LaunchRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            await Task.CompletedTask;

            string speechText = Translate.Get(nameof(Translations.Welcome), Request.Locale);

            SkillResponse response = ResponseBuilder.Ask(speechText, RequestHandlerHelper.GetDefaultReprompt(Request.Locale));
            return new OkObjectResult(response);
        }
    }
}
