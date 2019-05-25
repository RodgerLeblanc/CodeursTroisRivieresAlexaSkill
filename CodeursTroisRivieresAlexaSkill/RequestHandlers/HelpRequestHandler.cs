using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public class HelpRequestHandler : BaseRequestHandler<IntentRequest>
    {
        public HelpRequestHandler(IntentRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            await Task.CompletedTask;

            string speechText = "Vous pouvez demander : Quand est le prochain meetup, quel est le sujet de la prochaine rencontre, quel était le dernier événement. Que puis-je faire pour vous?";
            SkillResponse response = ResponseBuilder.Ask(speechText, RequestHandlerHelper.GetDefaultReprompt());

            return new OkObjectResult(response);
        }
    }
}
