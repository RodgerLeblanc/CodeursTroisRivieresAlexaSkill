﻿using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CellNinja.Localization.Resources;
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
        private const string _resource = "Codeurs-Trois-Rivieres/events";

        public NextMeetupRequestHandler(IntentRequest request) : base(request)
        {
        }

        public override async Task<IActionResult> GetResultAsync()
        {
            var request = GetRequest(_resource);

            request.AddQueryParameter("status", "upcoming");
            request.AddQueryParameter("scroll", "next_upcoming");
            request.AddQueryParameter("page", "1");

            List<MeetupEvent> events = await Client.GetAsync<List<MeetupEvent>>(request);

            SkillResponse response = GetResponseFromEvents(events);
            return new OkObjectResult(response);
        }

        private SkillResponse GetResponseFromEvents(List<MeetupEvent> events)
        {
            if (events == null || !events.Any(e => e != null))
            {
                string speechText = Translate.Get(nameof(Translations.NoFutureEvent), Request.Locale);
                return ResponseBuilder.Tell(speechText);
            }

            MeetupEvent nextEvent = events.First();

            IOutputSpeech speechResponse = GetSpeechResponse(nextEvent);

            return ResponseBuilder.Tell(speechResponse);
        }

        private IOutputSpeech GetSpeechResponse(MeetupEvent nextEvent)
        {
            string formattedDate = GetFormattedDate(nextEvent.LocalDate);
            string formattedTime = GetFormattedTime(nextEvent.Time);

            string speechText = Translate.Get(nameof(Translations.NextEvent), Request.Locale);
            string text = string.Format(speechText, formattedDate, formattedTime, nextEvent.Name);

            return new SsmlOutputSpeech { Ssml = $"<speak>{text}</speak>" };
        }
    }
}
