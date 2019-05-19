using Alexa.NET.Request.Type;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public abstract class BaseRequestHandler<TRequest> : IRequestHandler
        where TRequest : Request
    {
        public BaseRequestHandler(TRequest request)
        {
            Request = request;
        }

        protected TRequest Request { get; }

        public abstract Task<IActionResult> GetResultAsync();
    }
}
