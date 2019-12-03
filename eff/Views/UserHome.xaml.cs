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

        public UserHome(User user)
        {
            InitializeComponent();
            this.User = user;
            Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
            Application.Current.Properties["UserId"] = user.Id;

        }


        public  UserHome(string id)
        {
            Initialization = InitializeAsync(id);
            UserManager = UserManager.DefaultManager;
            InitializeComponent();


        }


        public Task Initialization { get; private set; }
        private async Task InitializeAsync(string id)
        {
            var user = await GetUser(id);
            this.User = user;
        }

        private async Task<User> GetUser(string id)
        {
           var user = await UserManager.GetUserById(id);
            return user[0];
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
