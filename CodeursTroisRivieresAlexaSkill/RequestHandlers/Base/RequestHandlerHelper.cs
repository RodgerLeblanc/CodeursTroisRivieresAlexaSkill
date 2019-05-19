using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public static class RequestHandlerHelper
    {
        private const string LastMeetupIntentName = "LastMeetupIntent";
        private const string NextMeetupIntentName = "NextMeetupIntent";
        private const string CancelIntentName = "AMAZON.CancelIntent";
        private const string HelpIntentName = "AMAZON.HelpIntent";
        private const string StopIntentName = "AMAZON.StopIntent";

        public static IRequestHandler GetFromRequest(Request request)
        {
            switch (request)
            {
                case LaunchRequest launchRequest:
                    return new LaunchRequestHandler(launchRequest);

                case IntentRequest intentRequest:
                    return GetRequestHandlerFromIntentRequest(intentRequest);
            }

            return null;
        }

        private static IRequestHandler GetRequestHandlerFromIntentRequest(IntentRequest intentRequest)
        {
            switch (intentRequest.Intent.Name)
            {
                case LastMeetupIntentName:
                    return new LastMeetupRequestHandler(intentRequest);

                case NextMeetupIntentName:
                    return new NextMeetupRequestHandler(intentRequest);

                case CancelIntentName:
                    return new CancelRequestHandler(intentRequest);

                case HelpIntentName:
                    return new HelpRequestHandler(intentRequest);

                case StopIntentName:
                    return new StopRequestHandler(intentRequest);

                default:
                    throw new ArgumentException("Could not find a matching handler for the IntentRequest Intent");
            }
        }

        public static Reprompt GetDefaultReprompt()
        {
            string repromptText = "Vous pouvez demander : " +
                "Quand est le prochain meetup, " +
                "quel est le sujet du prochain meetup, " +
                "quel était le dernier meetup.";

            return new Reprompt(repromptText);
        }

        public static async Task<bool> ValidateRequest(HttpRequest request, ILogger log, SkillRequest skillRequest)
        {
            request.Headers.TryGetValue("SignatureCertChainUrl", out var signatureChainUrl);
            if (string.IsNullOrWhiteSpace(signatureChainUrl))
            {
                log.LogError("Validation failed. Empty SignatureCertChainUrl header");
                return false;
            }

            Uri certUrl;
            try
            {
                certUrl = new Uri(signatureChainUrl);
            }
            catch
            {
                log.LogError($"Validation failed. SignatureChainUrl not valid: {signatureChainUrl}");
                return false;
            }

            request.Headers.TryGetValue("Signature", out var signature);
            if (string.IsNullOrWhiteSpace(signature))
            {
                log.LogError("Validation failed - Empty Signature header");
                return false;
            }

            request.Body.Position = 0;
            var body = await request.ReadAsStringAsync();
            request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                log.LogError("Validation failed - the JSON is empty");
                return false;
            }

            bool isTimestampValid = RequestVerification.RequestTimestampWithinTolerance(skillRequest);
            bool valid = await RequestVerification.Verify(signature, certUrl, body);

            if (!valid || !isTimestampValid)
            {
                log.LogError("Validation failed - RequestVerification failed");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
