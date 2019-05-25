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
    public class LastMeetupRequestHandler : BaseMeetupRequestHandler
    {
        private const string _resource = "Codeurs-Trois-Rivieres/events";

        public LastMeetupRequestHandler(IntentRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            var request = GetRequest(_resource);

            request.AddQueryParameter("status", "past");
            request.AddQueryParameter("scroll", "recent_past");
            request.AddQueryParameter("page", "1");

            List<MeetupEvent> events = await Client.GetAsync<List<MeetupEvent>>(request);

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

            MeetupEvent lastEvent = events.First();

            IOutputSpeech speechResponse = GetSpeechResponse(lastEvent);

            return ResponseBuilder.Tell(speechResponse);
        }

        private IOutputSpeech GetSpeechResponse(MeetupEvent lastEvent)
        {
            string formattedDate = GetFormattedDate(lastEvent.LocalDate);
            string formattedTime = GetFormattedTime(lastEvent.Time);

            string speechText = "Le dernier événement a eu lieu le {0} à {1}, le sujet était {2}.";
            string text = string.Format(speechText, formattedDate, formattedTime, lastEvent.Name);

            return new SsmlOutputSpeech { Ssml = $"<speak>{text}</speak>" };
        }
    }
}
