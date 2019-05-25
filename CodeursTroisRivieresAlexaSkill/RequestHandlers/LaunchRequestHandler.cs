using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
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

            string speechText = "Bienvenue dans la skill Alexa de Codeurs Trois-Rivières, que puis-je faire pour vous?";

            SkillResponse response = ResponseBuilder.Ask(speechText, RequestHandlerHelper.GetDefaultReprompt());
            return new OkObjectResult(response);
        }
    }
}
