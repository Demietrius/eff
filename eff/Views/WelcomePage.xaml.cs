﻿using System;
using eff.Views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Models;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();           
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        private async void OnGuestClicked(object sender, EventArgs e)
        {
          // await Navigation.PushAsync(new GuestPage());
        }
        private async void OnNearbyRestaurantsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPlaces(new User()));
        }
    }
}