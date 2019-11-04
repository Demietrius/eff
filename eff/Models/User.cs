using System;
using Newtonsoft.Json;

namespace eff.Models
{
    class User
    {
           
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public String Password { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /*
                [JsonProperty(PropertyName = "filters")]
                public string Filters { get; set; }

                [JsonProperty(PropertyName = "date")]
                public string Date { get; set; }

                [JsonProperty(PropertyName = "gamesPlayed")]
                public string GamesPlayed { get; set; }

                [JsonProperty(PropertyName = "groupId")]
                public string GroupId { get; set; }*/
    }
}
