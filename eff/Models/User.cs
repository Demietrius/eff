using System;
using Newtonsoft.Json;

namespace eff.Models
{
    class User
    {
           
        [JsonProperty(PropertyName = "userId")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public String Password { get; set; }

        [JsonProperty(PropertyName = "filter")]
        public string Filters { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "number of games played")]
        public string GamesPlayed { get; set; }

        [JsonProperty(PropertyName = "groupId")]
        public string GroupId { get; set; }
    }
}
