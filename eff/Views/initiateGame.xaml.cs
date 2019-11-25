using System;
using System.Collections.Generic;
using eff.ViewModels;
using Xamarin.Forms;

namespace eff.Views
{
    public partial class initiateGame : ContentPage
    {
        RoomManager room;
        
        public initiateGame()
        {
            InitializeComponent();
            room = RoomManager.DefaultManager;
            
        }

        protected async void startGameClicked()
        { 

        }
    }
}
