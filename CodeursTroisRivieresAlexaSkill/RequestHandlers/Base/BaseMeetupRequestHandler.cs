using Alexa.NET.Request.Type;
using RestSharp;

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
    }
}
