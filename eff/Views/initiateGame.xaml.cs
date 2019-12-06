using System;
using System.Collections.Generic;
using eff.ViewModels;
using Xamarin.Forms;
using eff.Models;

namespace eff.Views
{
    public partial class initiateGame : ContentPage
    {
        RoomManager RoomManager;
        Rooms Room;
        User user;

        public initiateGame(Rooms Room, User user)
        {
            InitializeComponent();
         
            RoomManager = RoomManager.DefaultManager;
            this.Room = Room;
            this.user = user;

            GetUsers();
            
        }

        private async void GetUsers() {
            while (true)
            {
                await RoomManager.GetUsersInRoom(Room);
                System.Threading.Thread.Sleep(1000);
            }
        }

        protected async void StartGameClicked(object sender, EventArgs e)
        {
            await RoomManager.StartGame(Room);
            await Navigation.PushAsync(new GetPlaces(user, Room));

        }
    }
}
