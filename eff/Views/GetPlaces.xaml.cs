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
		public List<String> LikedPlaces{ get; set; }

		YelpManager yelpManager;
        //TODO: Remove Roommanger
        RoomManager RoomManager;

		public GetPlaces(User user, Rooms Room)
		{
			InitializeComponent();
			yelpManager = YelpManager.DefaultManager;
			PlacesView.ItemsSource = yelpManager.Places;
			yelpManager.LikeCount = 0;
			yelpManager.NumberOfPlaces = 0;
			LikedPlaces = new List<string>();
            //TODO: removes Roommanger
            RoomManager = RoomManager.DefaultManager;

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
			if (e.CurrentSelection.Count() == 0)
				return;

			Place tempid = e.CurrentSelection[e.CurrentSelection.Count() - 1] as Place;
			if (CheckLikes() || LikedPlaces.Contains(tempid.id))
			{


				LikedPlaces = new List<string>(); ;
				{
					for (int index = 0; index < (int)e.CurrentSelection.Count(); index++)
					{
						Place TempPlace = e.CurrentSelection[index] as Place;
						LikedPlaces.Add(TempPlace.id.ToString());
					}
				}
				LikeCount = LikedPlaces.Count();
			}else
				errorAsync();
		}

		private async Task errorAsync()
		{
			await DisplayAlert("Alert", "You have liked too many dumb ass", "OK");
		}


		private bool CheckLikes()
		{
			if (LikeCount < (int)(0.30 * yelpManager.NumberOfPlaces))
				return true;
			else
				return false;
		}

        private async void submitClicked(object sender, EventArgs e)
        {
            


        }

	}
}