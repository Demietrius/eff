using System;
using System.Collections.Generic;
using System.Text;

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
        public int Filters { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "number of games played")]
        public int GamesPlayed { get; set; }

        [JsonProperty(PropertyName = "groupId")]
        public int GroupId { get; set; }
    }
}
