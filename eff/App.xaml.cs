using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Views;

namespace eff
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
            if (!isLoggedIn)
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                MainPage = new NavigationPage(new UserHome());
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
