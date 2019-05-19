using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CodeursTroisRivieresAlexaSkill.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using System.Globalization;
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
            request.AddQueryParameter("page", "1");

            List<MeetupEvent> events = await Client.GetAsync<List<MeetupEvent>>(request);

            SkillResponse response = GetResponseFromEvents(events);
            return new OkObjectResult(response);
        }

        private SkillResponse GetResponseFromEvents(List<MeetupEvent> events)
        {
            if (events == null || !events.Any(e => e != null))
            {
                return ResponseBuilder.Ask(
                    "Quelque chose cloche, je n'ai trouvé aucun Meetup antérieur.",
                    RequestHandlerHelper.GetDefaultReprompt());
            }

            MeetupEvent nextEvent = events.First();
            string speechResponse = GetSpeechResponse(nextEvent);

            return ResponseBuilder.Ask(speechResponse, RequestHandlerHelper.GetDefaultReprompt());
        }

        private string GetSpeechResponse(MeetupEvent nextEvent)
        {
            string formattedDate = nextEvent.LocalDate.ToString("dddd dd MMMM", CultureInfo.CreateSpecificCulture("fr-CA"));

            return $"Le dernier Meetup a eu lieu le {formattedDate} " +
                $"à {nextEvent.LocalTime}, " +
                $"le sujet était {nextEvent.Name}.";
        }
    }
}
