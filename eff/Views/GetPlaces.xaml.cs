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
using eff.ViewModels;

namespace eff.Views
{
   
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GetPlaces : ContentPage
	{
        YelpManager yelpManager;
        public GetPlaces(User user)
		{
			InitializeComponent();
            yelpManager = YelpManager.DefaultManager;
            PlacesView.ItemsSource = yelpManager.Places;
            yelpManager.LikeCount = 0;
            yelpManager.NumberOfPlaces = 0;
        }

        protected void RequestPlaces_Clicked(object sender, EventArgs e)
        {
            var location = yelpManager.GetUserLocationAsync();
            String latitude = location.Result.Item1;
            String longitude = location.Result.Item2;
            int radius = 1610;

            var searchString = yelpManager.GenerateYelpSearchString(latitude, longitude, radius);

            JObject joResponse = yelpManager.YelpWebRequest(searchString);

            yelpManager.ParseJObjectResponse(joResponse);

        }
        protected void LabelClicked(object sender, SelectionChangedEventArgs e)
        {
            var test = e.CurrentSelection;

            if (!yelpManager.CheckLikes())
                yelpManager.error();

            yelpManager.LikeCount++;

        }

    }
}