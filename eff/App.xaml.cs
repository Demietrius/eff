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
            string id = Current.Properties.ContainsKey("UserId") ? Convert.ToString(Current.Properties["UserId"]) : null;

            if (!isLoggedIn)
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                MainPage = new NavigationPage(new UserHome(id));
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
