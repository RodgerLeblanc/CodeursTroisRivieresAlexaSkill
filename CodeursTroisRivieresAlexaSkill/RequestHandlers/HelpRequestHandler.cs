﻿using Alexa.NET;
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

            SkillResponse response = ResponseBuilder.Ask("Annulation en cours.", RequestHandlerHelper.GetDefaultReprompt());
            return new OkObjectResult(response);
        }
    }
}
