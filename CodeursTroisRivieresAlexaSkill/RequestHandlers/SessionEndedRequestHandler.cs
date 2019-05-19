using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public class SessionEndedRequestHandler : BaseRequestHandler<SessionEndedRequest>
    {
        public SessionEndedRequestHandler(SessionEndedRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            await Task.CompletedTask;

            SkillResponse response = ResponseBuilder.Empty();
            return new OkObjectResult(response);
        }
    }
}
