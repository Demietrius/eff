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

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyRestaurants : ContentPage
    {
        public NearbyRestaurants()
        {
            InitializeComponent();
            PlacesView.ItemsSource = places;

            // ObservableCollection allows items to be added after ItemsSource
            // is set and the UI will react to changes
            places.Add(new Place { name = "Rob Finnerty" });
            places.Add(new Place { name = "Bill Wrestler" });
            places.Add(new Place { name = "Dr. Geri-Beth Hooper" });
            places.Add(new Place { name = "Dr. Keith Joyce-Purdy" });
            places.Add(new Place { name = "Sheri Spruce" });
            places.Add(new Place { name = "Burt Indybrick" });
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
            String categories = "food";
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

            HttpWebRequest webRequest = WebRequest.Create(placesSearchString) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Headers.Add("Authorization", "Bearer FxutAF1Xi3y_LjzpSKaV9aijfwKwcLLOs8APnHCcPu8FhZyKHvxPvS9_Fe_hx5jmcuPWr1nvBg6LJGiaiBp4YFi-uWVBAo-mqgNKD22bP0EdhsCdmOrveY6XX1myXXYx");
            webRequest.Method = "GET";

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            var content = stream.ReadToEnd();
            Console.Write(content);
            System.Diagnostics.Debug.WriteLine(content);
            System.Diagnostics.Debug.WriteLine("FINDME");
        }

        ObservableCollection<Place> places = new ObservableCollection<Place>();
        public ObservableCollection<Place> Places { get { return places; } }

/*        public NearbyRestaurants()
        {
            PlacesView.ItemsSource = places;

            // ObservableCollection allows items to be added after ItemsSource
            // is set and the UI will react to changes
            places.Add(new Place { name = "Rob Finnerty" });
            places.Add(new Place { name = "Bill Wrestler" });
            places.Add(new Place { name = "Dr. Geri-Beth Hooper" });
            places.Add(new Place { name = "Dr. Keith Joyce-Purdy" });
            places.Add(new Place { name = "Sheri Spruce" });
            places.Add(new Place { name = "Burt Indybrick" });
        }*/
    }

}