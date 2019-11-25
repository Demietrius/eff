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

        public initiateGame(Rooms Room)
        {
            InitializeComponent();
            RoomManager = RoomManager.DefaultManager;
            this.Room = Room;
            
        }

        protected async void startGameClicked()
        {
           await RoomManager.StartGame(Room);

        }
    }
}
