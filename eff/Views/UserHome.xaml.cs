using System;
using System.Collections.Generic;
using eff.Models;
using eff.Views;

using Xamarin.Forms;

namespace eff.Views
{
    public partial class UserHome : ContentPage
    {
        public UserHome()
        {
            InitializeComponent();
            ////////////////////////////////testing
            var user = new User() { Username = "testing" };

            //////////////////////////////////////
            Lbl_user.SetBinding(Label.TextProperty,new Binding(user.Username));

        }

        
        private async void OnJoinClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPlaces());
         //   await Navigation.PushAsync(new GuestPage());
        }

       

        private async void OnCreateLobbyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateLobby());
           
        }
    }
}
