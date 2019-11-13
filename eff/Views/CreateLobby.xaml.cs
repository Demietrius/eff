using eff.Models;
using eff.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eff.Views
{
    public partial class CreateLobby : ContentPage
    {
        RoomManager RoomManager;
        UserManager userManager;
        public CreateLobby()
        {
            InitializeComponent();
            RoomManager = RoomManager.DefaultManager;
            userManager = UserManager.DefaultManager;
        }

        private async void Submit(object sender, EventArgs e)
        {
            var Room = new Rooms() {
                RoomName = Entry_RoomName.Text,
                City = Entry_City.Text,
                Distance = Pkr_NumPlaces.ToString(),
                NumberOfResturants = Pkr_NumPlaces.ToString(),
                Price = Pkr_Price.ToString(),
                };

           await RoomManager.InsertRoom(Room);



        }
    }
}
