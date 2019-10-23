using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eff.Views
{
    public partial class UserHome : ContentPage
    {
        public UserHome()
        {
            InitializeComponent();
        }
        private async void OnJoinClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GuestPage());
        }

        private async void OnCreateLobbyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateLobby());
        }
    }
}
