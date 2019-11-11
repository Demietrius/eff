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

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetPlaces : ContentPage
    {
        public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
        public GetPlaces()
        {
            InitializeComponent();
            PlacesView.ItemsSource = Places;
        }

        protected async void RequestPlaces_Clicked(object sender, EventArgs e)
        {
            String placesSearchString = await GenerateSearchStringAsync();
            Console.WriteLine(placesSearchString + " FINDME");

            HttpWebRequest webRequest = WebRequest.Create(placesSearchString) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Headers.Add("Authorization", "Bearer FxutAF1Xi3y_LjzpSKaV9aijfwKwcLLOs8APnHCcPu8FhZyKHvxPvS9_Fe_hx5jmcuPWr1nvBg6LJGiaiBp4mesYFi-uWVBAo-mqgNKD22bP0EdhsCdmOrveY6XX1myXXYx");
            webRequest.Method = "GET";

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            var content = stream.ReadToEnd();

            JObject joResponse = JObject.Parse(content);
            JArray array = (JArray)joResponse["businesses"];
            string businesses = array[0].ToString();

            foreach (JToken bus in joResponse.SelectToken("businesses"))
            {
                string b = (string)bus.SelectToken("name");
                string c = (string)bus.SelectToken("image_url");
                string d = (string)bus.SelectToken("rating");
                Places.Add(new Place { name = b, image_url = c, rating = d });
            }
        }

        public async Task<String> GenerateSearchStringAsync()
        {
            var gpsLocation = await GetLocationAsync();
            String latitude = gpsLocation.Item1.ToString();
            String longitude = gpsLocation.Item2.ToString();
            longitude = longitude.Substring(0, 5);
            latitude = latitude.Substring(0, 5);
            String categories = "Restaurant";

            int radius = 1610;

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
        public async Task<Tuple<string, string>> GetLocationAsync()
        {
            //First attempts to get the last cached location by phone
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    return Tuple.Create(location.Latitude.ToString(), location.Longitude.ToString());
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                // User will need to enter a zip code
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            //If no cached location is available, get current location (may take some time)
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    return Tuple.Create(location.Latitude.ToString(), location.Longitude.ToString());
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return null;
        }

    }

}