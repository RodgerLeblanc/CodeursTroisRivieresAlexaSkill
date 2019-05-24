using Alexa.NET.Request.Type;
using CellNinja.Localization.Resources;
using RestSharp;
using System;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public abstract class BaseMeetupRequestHandler : BaseRequestHandler<IntentRequest>
    {
        private const string _baseUrl = "https://api.meetup.com/";

        public BaseMeetupRequestHandler(IntentRequest request) : base(request)
        {
            Client = new RestClient(_baseUrl);
        }

        protected IRestClient Client { get; }

        protected IRestRequest GetRequest(string resource)
        {
            var request = new RestRequest(resource, DataFormat.Json);

            request.AddQueryParameter("sign", "true");
            request.AddQueryParameter("photo-host", "public");

            return request;
        }

        protected string GetFormattedDate(DateTimeOffset localDate)
        {
            return $"<say-as interpret-as=\"date\">{localDate.ToString("????MMdd")}</say-as>";
        }

        protected string GetFormattedTime(long msSinceEpoch)
        {
            DateTime time = GetEasternTimeFromEpoch(msSinceEpoch);
            string timeFormat = Translate.Get(nameof(Translations.TimeFormat), Request.Locale);
            return time.ToString(timeFormat);
        }

        private DateTime GetEasternTimeFromEpoch(long msSinceEpoch)
        {
            DateTime timeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(msSinceEpoch);
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
        }
    }
}
