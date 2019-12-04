using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Models;
using System.Security.Cryptography;
using eff.ViewModels;
using System.Text;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserManager userManger;
        public LoginPage()
        {
            InitializeComponent();
            userManger = UserManager.DefaultManager;
            if (Device.RuntimePlatform == Device.Android)
            {
                Entry_Username.TextColor = Color.White;
                Entry_Password.TextColor = Color.White;
            }
        }
        private async void Login(object sender, EventArgs e)
        {

            var tempUser = new User { Username = Entry_Username.Text.ToLower(), Password = Entry_Password.Text};
            // hashing password
            using (MD5 md5Hash = MD5.Create())
            {
                tempUser.Password = GetMd5Hash(md5Hash, tempUser.Password);

            }

            // query the database to check if logged in
            var user = await userManger.Login(tempUser);
            if (user != null)
            {
                await Navigation.PushAsync(new UserHome(user));
            }
            else
                Error(user);
        }



        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateUser());
        }



        private void Error(User user)
        {

        }

       
        /// <summary>
        /// hasing methods
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}

