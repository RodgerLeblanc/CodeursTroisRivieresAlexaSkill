using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CodeursTroisRivieresAlexaSkill.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public class NextMeetupRequestHandler : BaseMeetupRequestHandler
    {
        private readonly string[] _resources = new string[2]
        {
            "Cafe-et-coding/events",
            "Codeurs-Trois-Rivieres/events"
        };

        public NextMeetupRequestHandler(IntentRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            List<MeetupEvent> events = new();

            foreach (var resource in _resources)
            {
                var request = GetRequest(resource);

                request.AddQueryParameter("status", "upcoming");
                request.AddQueryParameter("scroll", "next_upcoming");
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
                string speechText = "Il n'y a présentement pas de prochain événement annoncé.";
                return ResponseBuilder.Tell(speechText);
            }

            MeetupEvent nextEvent = events
                .OrderBy(e => e.Time)
                .First();

            IOutputSpeech speechResponse = GetSpeechResponse(nextEvent);

            return ResponseBuilder.Tell(speechResponse);
        }

        private IOutputSpeech GetSpeechResponse(MeetupEvent nextEvent)
        {
            string formattedDate = GetFormattedDate(nextEvent.Time);
            string formattedTime = GetFormattedTime(nextEvent.Time);

            string speechText = "Le prochain événement est {2} et aura lieu le {0} à {1}.";
            string text = string.Format(speechText, formattedDate, formattedTime, nextEvent.Name);

            return new SsmlOutputSpeech { Ssml = $"<speak>{text}</speak>" };
        }
    }
}
