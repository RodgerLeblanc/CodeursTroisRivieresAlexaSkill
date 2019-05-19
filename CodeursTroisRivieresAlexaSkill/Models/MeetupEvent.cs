using Newtonsoft.Json;
using System;

namespace CodeursTroisRivieresAlexaSkill.Models
{
    public partial class MeetupEvent
    {
        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date_in_series_pattern")]
        public bool DateInSeriesPattern { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("local_date")]
        public DateTimeOffset LocalDate { get; set; }

        [JsonProperty("local_time")]
        public string LocalTime { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("utc_offset")]
        public long UtcOffset { get; set; }

        [JsonProperty("waitlist_count")]
        public long WaitlistCount { get; set; }

        [JsonProperty("yes_rsvp_count")]
        public long YesRsvpCount { get; set; }

        [JsonProperty("venue")]
        public Venue Venue { get; set; }

        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("how_to_find_us")]
        public string HowToFindUs { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }

    public partial class Group
    {
        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("join_mode")]
        public string JoinMode { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("urlname")]
        public string Urlname { get; set; }

        [JsonProperty("who")]
        public string Who { get; set; }

        [JsonProperty("localized_location")]
        public string LocalizedLocation { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }

    public partial class Venue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("repinned")]
        public bool Repinned { get; set; }

        [JsonProperty("address_1")]
        public string Address1 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("localized_country_name")]
        public string LocalizedCountryName { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
