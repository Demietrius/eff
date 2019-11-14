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
			var request = new GeolocationRequest(GeolocationAccuracy.High);
			var location = await Geolocation.GetLocationAsync(request);
			String latitude = location.Latitude.ToString();
			String longitude = location.Longitude.ToString();
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
			Console.WriteLine(placesSearchString + "  FINDME");
			HttpWebRequest webRequest = WebRequest.Create(placesSearchString) as HttpWebRequest;
			webRequest.Timeout = 20000;
			webRequest.Headers.Add("Authorization", "Bearer FxutAF1Xi3y_LjzpSKaV9aijfwKwcLLOs8APnHCcPu8FhZyKHvxPvS9_Fe_hx5jmcuPWr1nvBg6LJGiaiBp4YFi-uWVBAo-mqgNKD22bP0EdhsCdmOrveY6XX1myXXYx");
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

		protected void LabelClicked(object sender, EventArgs e)
		{
			var entity = ((Frame)sender);

            if (entity.BackgroundColor != Color.Orange)
            {
                entity.BackgroundColor = Color.Orange;   
            }

            else
            {
                    entity.BackgroundColor = Color.FromHex("#6e6e6c");
            }
        }

	}
}