using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
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

            string speechText = "Au revoir";
            SkillResponse response = ResponseBuilder.Tell(speechText);
            return new OkObjectResult(response);
        }
    }
}
