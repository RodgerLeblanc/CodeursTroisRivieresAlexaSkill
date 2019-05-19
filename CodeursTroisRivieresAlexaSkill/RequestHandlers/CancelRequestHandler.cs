using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public class CancelRequestHandler : BaseRequestHandler<IntentRequest>
    {
        public CancelRequestHandler(IntentRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            await Task.CompletedTask;

            SkillResponse response = ResponseBuilder.Tell("Annulation en cours.");
            return new OkObjectResult(response);
        }
    }
}
