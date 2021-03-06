﻿using System;
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
        public double SecLeft { get; set; }
        public int minl { get; set; }
        public int secl { get; set; }
        public List<String> LikedPlaces{ get; set; }


		YelpManager yelpManager;
		RoomManager RoomManager;
        Rooms Room;
        User user;

		public GetPlaces(User user, Rooms Room)
		{
			InitializeComponent();
			yelpManager = YelpManager.DefaultManager;
            yelpManager.Places.Clear();
            PlacesView.ItemsSource = yelpManager.Places;
			yelpManager.LikeCount = 0;
			yelpManager.NumberOfPlaces = 0;
			LikedPlaces = new List<string>();
            RoomManager = RoomManager.DefaultManager;
            this.Room = Room;

            //TimeSpan span = DateTime.Now.AddMinutes(.1).Subtract(DateTime.Now);
            TimeSpan span = Convert.ToDateTime(Room.Date).Subtract(DateTime.Now);
            SecLeft = span.TotalSeconds;
            //RequestPlaces_ClickedAsync();
            RequestPlaces(this.Room);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                minl = (int)SecLeft / 60;
                secl = (int)SecLeft % 60;
                RoundTimer.Text = minl + ":" + secl;
                SecLeft--;
                if (SecLeft <= 0)
                {
                    submitClicked(null, null);

                    return false;
                }
                return true;
            });
        }

       

        //is this being used anymore?
		protected async void RequestPlaces_ClickedAsync()
		{
			String location = await yelpManager.GetUserLocationAsync();
			var strlist = location.Split(',');
			String latitude = strlist[0];
			String longitude = strlist[1];
			int radius = 1610;
			int maxresults = 20;
			int price = 4;

			var searchString = yelpManager.GenerateYelpSearchString(latitude, longitude, radius, maxresults, price);

			JObject joResponse = yelpManager.YelpWebRequest(searchString);

			yelpManager.ParseJObjectResponse(joResponse);
		}

        protected void RequestPlaces(Rooms room)
        {
            String zip = room.City;
            if(zip == null)
            {
                RequestPlaces_ClickedAsync();
                return;
            }
            String maxresults = room.NumberOfResturants;
            String radius = room.Distance;
            Int32.TryParse(room.Price, out int price);

            var searchString = yelpManager.GenerateYelpSearchString(zip, radius, maxresults, price);

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
			await DisplayAlert("whoa, there", "You have used your max amount of likes", "dang");
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
            string tempRound;
            int GameRound;

            bool resturantsAdded =  await RoomManager.AddResturants(Room, LikedPlaces);

            Room = await RoomManager.JoinRoom(Room.RoomNumber, Room.PIN);

            tempRound = Application.Current.Properties["GameRound"].ToString();
            GameRound = System.Convert.ToInt32(tempRound);
            GameRound++;
            Application.Current.Resources["GameRound"] = GameRound.ToString();

            await Navigation.PushAsync(new guestLanding(Room, user));

		}
	}
}