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
            string id = Application.Current.Properties["ID"].ToString();
            Console.WriteLine(id);
            this.room = room;
            RoomManager = RoomManager.DefaultManager;
            this.Room = Room;
            
        }

        protected async void StartGameClicked(object sender, EventArgs e)
        {
         await RoomManager.StartGame(Room);

        }
    }
}
