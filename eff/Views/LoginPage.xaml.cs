using System;
using System.Collections.Generic;

using Xamarin.Forms;
using eff.Models;


namespace eff.Views
{
    
    public partial class LoginPage : ContentPage
    {
        Database_connection database_Connection = new Database_connection();

        public LoginPage()
        {
            InitializeComponent();
        }
        private async void Login(object sender, EventArgs e)
        {
            string message = database_Connection.TestDatabaseConnection();

        }
      
    }
}
