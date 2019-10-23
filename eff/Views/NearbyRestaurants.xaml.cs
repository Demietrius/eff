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

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyRestaurants : ContentPage
    {
        public NearbyRestaurants()
        {
            InitializeComponent();
        }

        private void RequestCompleted(IAsyncResult result)
        {
            var request = (HttpWebRequest)result.AsyncState;
            var response = (HttpWebResponse)request.EndGetResponse(result);
            using (var stream = response.GetResponseStream())
            {
                var r = new StreamReader(stream);
                var resp = r.ReadToEnd();
            }

        }

        protected async void RequestPlaces_Clicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1000), null, true);
            //String latitude =  position.Latitude.ToString();
            //String longitude = position.Longitude.ToString();
            String latitude = "39.032329";
            String longitude = "-94.348950";

            //easy-to-copy example displaying the returned json
            //https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=39.032329,-94.348950&radius=1610&sensor=true&type=restaurant&key=AIzaSyBaWchHhb7uEWp81QYBVGLf86WlKx1p1OI
            //radius is measured in meters, max is 50,000
            //1610 meters = 1 mile
            string placesSearchString = "https://maps.googleapis.com/maps/api/place/nearbysearch/" +
                "json?location=" + latitude + "," + longitude +
                "&radius=1610&sensor=true" +
                "&type=restaurant" +
                "&key=AIzaSyBaWchHhb7uEWp81QYBVGLf86WlKx1p1OI";

            HttpWebRequest webRequest = WebRequest.Create(placesSearchString) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Method = "GET";

            webRequest.BeginGetResponse(new AsyncCallback(RequestCompleted), webRequest);
        }
    }

}