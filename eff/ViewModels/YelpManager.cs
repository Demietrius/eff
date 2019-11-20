using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Collections.ObjectModel;
using eff.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace eff.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    class YelpManager
    {
        static YelpManager defaultInstance = new YelpManager();
        public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
        public int LikeCount { get; set; }
        public int NumberOfPlaces { get; set; }


        public static YelpManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public async Task<string> GetUserLocationAsync()
        {
            //More accurate/recent, may take some time for a response
            var request = new GeolocationRequest(GeolocationAccuracy.High,TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);

            //faster, will return null if there is no cached location
            //var location = await Geolocation.GetLastKnownLocationAsync();
            var latstr = location.Latitude.ToString();
            var lonstr = location.Longitude.ToString();
            return  latstr + ',' + lonstr;
        }

        public string GenerateYelpSearchString(String latitude, String longitude, int radius)
        {
            String categories = "Restaurant";
            //documentation https://www.yelp.com/developers/documentation/v3/business_search
            //radius is measured in meters, max is 40,000(~25 miles)
            //1610 meters = 1 mile
            //Yelp api key : FxutAF1Xi3y_LjzpSKaV9aijfwKwcLLOs8APnHCcPu8FhZyKHvxPvS9_Fe_hx5jmcuPWr1nvBg6LJGiaiBp4YFi-uWVBAo-mqgNKD22bP0EdhsCdmOrveY6XX1myXXYx
            string placesSearchString = "https://api.yelp.com/v3/businesses/search" +
                "?latitude=" + latitude +
                "&longitude=" + longitude +
                "&categories=" + categories +
                "&radius=" + radius;

            return placesSearchString;
        }

        public JObject YelpWebRequest(String searchString)
        {
            HttpWebRequest webRequest = WebRequest.Create(searchString) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Headers.Add("Authorization", "Bearer FxutAF1Xi3y_LjzpSKaV9aijfwKwcLLOs8APnHCcPu8FhZyKHvxPvS9_Fe_hx5jmcuPWr1nvBg6LJGiaiBp4YFi-uWVBAo-mqgNKD22bP0EdhsCdmOrveY6XX1myXXYx");
            webRequest.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            var content = stream.ReadToEnd();

            return JObject.Parse(content);
        }

        public void ParseJObjectResponse(JObject joResponse)
        {
            foreach (JToken bus in joResponse.SelectToken("businesses"))
            {
                string b = (string)bus.SelectToken("name");
                string c = (string)bus.SelectToken("image_url");
                if (c.Length < 2)
                {
                    c = "filler.jpg";
                }
                string d = (string)bus.SelectToken("rating");
                Places.Add(new Place { name = b, image_url = c, rating = d });
            }
            NumberOfPlaces = Places.Count;
        }

        

        public void error() { }

        public bool CheckLikes()
        {
            if (LikeCount < (int)(0.30 * NumberOfPlaces))
                return true;
            else
                return false;
        }

    }

}
