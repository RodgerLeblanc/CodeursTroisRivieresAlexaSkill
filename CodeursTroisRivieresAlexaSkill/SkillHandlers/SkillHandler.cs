using Alexa.NET.Request.Type;
using CodeursTroisRivieresAlexaSkill.RequestHandlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.SkillHandlers
{
    public class SkillHandler : ISkillHandler
    {
        private IRequestHandler _requestHandler;

        public SkillHandler(Request request)
        {
            _requestHandler = RequestHandlerHelper.GetFromRequest(request);
        }

        public Task<IActionResult> GetResultAsync()
        {
            return _requestHandler.GetResultAsync();
        }
    }
}
