using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CellNinja.Localization.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public class StopRequestHandler : BaseRequestHandler<IntentRequest>
    {
        public StopRequestHandler(IntentRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            await Task.CompletedTask;

            string speechText = Translate.Get(nameof(Translations.Goodbye), Request.Locale);
            SkillResponse response = ResponseBuilder.Tell(speechText);
            return new OkObjectResult(response);
        }
    }
}
