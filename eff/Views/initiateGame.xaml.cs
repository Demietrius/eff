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
        Rooms room;
        
        public initiateGame(Rooms room)
        {
            InitializeComponent();
            string id = Application.Current.Properties["ID"].ToString();
            Console.WriteLine(id);
            this.room = room;
            RoomManager = RoomManager.DefaultManager;
            
        }

        protected async void StartGameClicked(object sender, EventArgs e)
        { 

        }
    }
}
