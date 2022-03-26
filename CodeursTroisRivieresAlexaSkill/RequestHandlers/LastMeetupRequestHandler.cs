using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CodeursTroisRivieresAlexaSkill.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public class LastMeetupRequestHandler : BaseMeetupRequestHandler
    {
        private readonly string[] _resources = new string[2] 
        {
            "Cafe-et-coding/events",
            "Codeurs-Trois-Rivieres/events"
        };

        public LastMeetupRequestHandler(IntentRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            List<MeetupEvent> events = new();

            foreach (var resource in _resources)
            {
                var request = GetRequest(resource);

                request.AddQueryParameter("status", "past");
                request.AddQueryParameter("scroll", "recent_past");
                request.AddQueryParameter("page", "1");

                var resourceEvents = await Client.GetAsync<List<MeetupEvent>>(request);

                events.AddRange(resourceEvents);
            }

            SkillResponse response = GetResponseFromEvents(events);
            return new OkObjectResult(response);
        }

        private SkillResponse GetResponseFromEvents(List<MeetupEvent> events)
        {
            if (events == null || !events.Any(e => e != null))
            {
                string speechText = "Quelque chose cloche, je n'ai trouvé aucun événement antérieur.";
                return ResponseBuilder.Tell(speechText);
            }

            MeetupEvent lastEvent = events
                .OrderByDescending(e => e.Time)
                .First();

            IOutputSpeech speechResponse = GetSpeechResponse(lastEvent);

            return ResponseBuilder.Tell(speechResponse);
        }

        private IOutputSpeech GetSpeechResponse(MeetupEvent lastEvent)
        {
            string formattedDate = GetFormattedDate(lastEvent.Time);
            string formattedTime = GetFormattedTime(lastEvent.Time);

            string speechText = "Le dernier événement était {2} et a eu lieu le {0} à {1}.";
            string text = string.Format(speechText, formattedDate, formattedTime, lastEvent.Name);

            return new SsmlOutputSpeech { Ssml = $"<speak>{text}</speak>" };
        }
    }
}
