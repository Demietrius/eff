using System;
using System.Collections.Generic;
using eff.Models;
using eff.Views;


using Xamarin.Forms;

namespace eff.Views
{
    public partial class UserHome : ContentPage
    {
        User User;
        public UserHome()
        {
            InitializeComponent();
            Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
            ////////////////////////////////testing
            //var user = new User() { Username = "testing" };

            ////////////////////////////////////////
            //Lbl_user.SetBinding(Label.TextProperty,new Binding(user.Username));
        }
        public UserHome(User user)
        {
            InitializeComponent();
            Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
            this.User = user;
            ////////////////////////////////testing
            //var user = new User() { Username = "testing" };

            ////////////////////////////////////////
            //Lbl_user.SetBinding(Label.TextProperty,new Binding(user.Username));

        }

        
        private async void OnJoinClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPlaces(User));
        }

       

        private async void OnCreateLobbyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateLobby(User));
           
        }

        private async void OnNearbyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Nearby());
        }

        private async void Logout(object sender, EventArgs e)
        {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            await Navigation.PushAsync(new WelcomePage());
        }
    }
}
