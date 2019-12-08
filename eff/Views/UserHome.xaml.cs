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
        User TempUser;
        UserManager UserManager;
        string id;

        public UserHome(User user)
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey("IsLoggedIn") == true)
            {
                TempUser = new User() { Password = Application.Current.Properties["Password"].ToString(), Email = Application.Current.Properties["Email"].ToString() };

            }
            else
            {
            this.TempUser = user;
            Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
            Application.Current.Properties["Email"] = user.Email;
            Application.Current.Properties["Password"] = user.Password;
            }
        }


        private async void OnJoinClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new GuestPage(TempUser));
        }


        private async void OnCreateLobbyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateLobby(TempUser));
        }        

        private async void OnNearbyClicked(object sender, EventArgs e)
        {
        await Navigation.PushAsync(new Nearby());
        }

        private async void Logout(object sender, EventArgs e)
        {
        //var user = await UserManager.GetUserById(id);
        Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            await Navigation.PushAsync(new WelcomePage());
        }
    }
}
