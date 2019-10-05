using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Models;


namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
       // Database_connection database_Connection = new Database_connection();

        public LoginPage()
        {
            InitializeComponent();
        }
        private async void Login(object sender, EventArgs e)
        {
        //    string message = database_Connection.TestDatabaseConnection();

        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateUser());
        }

    }
}
