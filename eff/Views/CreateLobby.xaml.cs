﻿using eff.Models;
using eff.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eff.Views
{
    public partial class CreateLobby : ContentPage
    {
        User User;
        RoomManager RoomManager;
        UserManager userManager;
        public CreateLobby(User User) 
        {
            InitializeComponent();
            SetAndroid();
            RoomManager = RoomManager.DefaultManager;
            userManager = UserManager.DefaultManager;
            this.User = User;
        }

        private void SetAndroid()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Entry_RoomName.PlaceholderColor = Color.NavajoWhite;
                Entry_RoomName.TextColor = Color.NavajoWhite;
                Entry_City.PlaceholderColor = Color.NavajoWhite;
                Entry_City.TextColor = Color.NavajoWhite;
                Pkr_Distance.TextColor = Color.NavajoWhite;
                Pkr_NumPlaces.TextColor = Color.NavajoWhite;
                Pkr_Price.TextColor = Color.NavajoWhite;
                Pkr_RoundTime.TextColor = Color.NavajoWhite;
            }
        }

        private async void Submit(object sender, EventArgs e)
        {
            var Room = new Rooms() {
                RoomName = Entry_RoomName.Text,
                City = Entry_City.Text,
                Distance = convertDistance(Pkr_Distance.SelectedItem.ToString()),
                NumberOfResturants = convertMaxRestaurants(Pkr_NumPlaces.SelectedItem.ToString()),
                Price = convertPrice(Pkr_Price.SelectedItem.ToString()),
                RoundTime = convertRoundTime(Pkr_RoundTime.SelectedItem.ToString()),
                RoomNumber = generateId(),
                PIN = generatePIN()
                };

            await RoomManager.InsertRoom(Room);
            var newGame = new initiateGame(Room, User);
            newGame.BindingContext = Room;
            await Navigation.PushAsync(newGame);

        }
         private string generateId()
        {
            Random generator = new Random();
            String r = generator.Next(010000, 999999).ToString("D6");
            return r;
        }

        private string generatePIN()
        {
            Random generator = new Random();
            String r = generator.Next(0000, 9999).ToString("D4");
            return r;
        }

        private string convertMaxRestaurants(string maxString)
        {
            switch(maxString.ToLower())
            {
                case "5":
                    return "5";
                case "10":
                    return "10";
                case "15":
                    return "15";
                case "max":
                    return "50";
                default:
                    return "50";
            }
        }

        private string convertPrice(string priceString)
        {
            switch(priceString.ToLower())
            {
                case "low":
                    return "2";
                case "medium":
                    return "3";
                case "high":
                    return "4";
                default:
                    return "4";
            }
        }
        public string convertRoundTime(string RoundTime)
        {
            switch (RoundTime.ToLower())
            {
                case "3 minutes":
                    return "3";
                case "5 minutes":
                    return "5";
                case "10 minutes":
                    return "10";
                case "15 minutes":
                    return "15";
                default:
                    return "3";
            }
        }

        private string convertDistance(string milesString)
        {
            //multiply this by the number of miles desired
            int miles_to_meters = 1610;
            int index = milesString.LastIndexOf(" ");
            int milesInt;
            //gets the "3" out of "3 miles" (or any other number)
            if (!Int32.TryParse(milesString.Substring(0, index), out milesInt))
            {
                //if tryparse fails, defaults to 1 mile instead of crashing
                milesInt = 1;
            }
            return (milesInt * miles_to_meters).ToString();
        }
    }
}
