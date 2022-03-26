using Alexa.NET.Request.Type;
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

        protected RestClient Client { get; }

        protected RestRequest GetRequest(string resource)
        {
            var request = new RestRequest(resource);

            request.AddQueryParameter("sign", "true");
            request.AddQueryParameter("photo-host", "public");

            return request;
        }

        protected string GetFormattedDate(long msSinceEpoch)
        {
            DateTime date = GetEasternTimeFromEpoch(msSinceEpoch);
            string dateFormat = date.Year == DateTime.Now.Year ? "????MMdd" : "yyyyMMdd";
            return $"<say-as interpret-as=\"date\">{date.ToString(dateFormat)}</say-as>";
        }

        protected string GetFormattedTime(long msSinceEpoch)
        {
            DateTime time = GetEasternTimeFromEpoch(msSinceEpoch);
            string timeFormat = "H:mm";
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
