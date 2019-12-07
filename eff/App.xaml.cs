using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Views;
using eff.Models;
using eff.ViewModels;

namespace eff
{
    public partial class App : Application
    {
        UserManager userManager;

        public App()
        {
            InitializeComponent();

            userManager = UserManager.DefaultManager;
            //DependencyService.Register<MockDataStore>();
            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
            string Email = Current.Properties.ContainsKey("Email") ? Convert.ToString(Current.Properties["Email"]) : null;
            string Password = Current.Properties.ContainsKey("Password") ? Convert.ToString(Current.Properties["Password"]) : null;

            if (isLoggedIn && Email != null && Password != null)
            {
                User TempUser = new User() { Email = Email, Password = Password };
                var user = userManager.Login(TempUser);
                MainPage = new NavigationPage(new UserHome(TempUser));
            }
            else
            {
                MainPage = new NavigationPage(new WelcomePage());

            }
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
