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
		
		public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
		public int LikeCount { get; set; }
		public int NumberOfPlaces { get; set; }
		public List<String> LikedPlaces{ get; set; }
		public GetPlaces(User user)
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

        protected async void RequestPlaces_ClickedAsync(object sender, EventArgs e)
        {
            String location = await yelpManager.GetUserLocationAsync();
            var strlist = location.Split(',');
            String latitude = strlist[0];
            String longitude = strlist[1];
            int radius = 1610;
            int maxresults = 20;

            var searchString = yelpManager.GenerateYelpSearchString(latitude, longitude, radius, maxresults);

            JObject joResponse = yelpManager.YelpWebRequest(searchString);

            yelpManager.ParseJObjectResponse(joResponse);

        }
        protected void LabelClicked(object sender, SelectionChangedEventArgs e)
        {
            var test = e.CurrentSelection;

            if (!yelpManager.CheckLikes())
                yelpManager.error();

		private async Task errorAsync() {
            await DisplayAlert("Alert", "You have liked too many dumb ass", "OK");
        }

        }

        protected void submitClicked(object sender, EventArgs e)
        {

        }

	}
}