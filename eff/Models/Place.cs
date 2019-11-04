using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace eff.Models
{
    public class Place
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name {get; set;}

        [JsonProperty(PropertyName = "image_url")]
        public string image_url {get; set;}

        [JsonProperty(PropertyName = "is_closed")]
        public string is_closed {get; set;}

        [JsonProperty(PropertyName = "url")]
        public string url {get; set;}

        [JsonProperty(PropertyName = "rating")]
        public string rating {get; set;}

        [JsonProperty(PropertyName = "coordinates")]
        public string coordinates {get; set;}

        [JsonProperty(PropertyName = "price")]
        public string price {get; set;}

        [JsonProperty(PropertyName = "display_address")]
        public string display_address {get; set;}

        [JsonProperty(PropertyName = "display_phone")]
        public string display_phone {get; set;}

        [JsonProperty(PropertyName = "distance")]
        public string distance {get; set;}
    }
}
