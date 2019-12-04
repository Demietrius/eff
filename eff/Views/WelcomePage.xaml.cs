using System;
using eff.Views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Models;
using eff.ViewModels;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        UserManager userManger;

        public WelcomePage()
        {
            InitializeComponent();
            userManger = UserManager.DefaultManager;
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        private async void OnGuestClicked(object sender, EventArgs e)
        {
            User TempUser = new User() { IsGuest = true };
            var user = await userManger.InsertUser(TempUser);

           await Navigation.PushAsync(new GuestPage(user));
        }
        private async void OnNearbyRestaurantsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPlaces(new User(), new Rooms()));
        }
    }
}