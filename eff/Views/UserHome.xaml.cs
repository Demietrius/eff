using System;
using System.Collections.Generic;
using eff.Models;
using eff.Views;


using Xamarin.Forms;
using eff.ViewModels;
using System.Threading.Tasks;

namespace eff.Views
{
    public partial class UserHome : ContentPage
    {
        User User;
        UserManager UserManager;
        public UserHome(string id)
        {
            InitializeComponent();
            UserManager = UserManager.DefaultManager;

            var user = GetUser(id);

            
        }
        public UserHome(User user)
        {
            InitializeComponent();
            this.User = user;
            Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
            Application.Current.Properties["UserId"] = user.Id;

        }

        private async Task<List<User>> GetUser(string id)
        {
           var user = await UserManager.GetUserById(id);
            return user;
        }


        private async void OnJoinClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GuestPage(User));
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
