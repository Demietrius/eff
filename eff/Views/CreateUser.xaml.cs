using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using eff.Models;
using eff.ViewModels;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace eff.Views
{
    public partial class CreateUser : ContentPage
    {

        UserManager userManger;
        StringBuilder sBuilder;
        public CreateUser()
        {
            InitializeComponent();
            userManger = UserManager.DefaultManager;
            sBuilder = new StringBuilder();
        }

      

        public async void InsertUser(object sender, EventArgs e)
        {

           
            var user = new User { Username = Entry_Username.Text, Password = Entry_Password.Text};


            var Userlist = await userManger.GetUserByEmail(user.Username);
            if (Userlist.Count == 0)
            {




                if (CheckEmail(Entry_Username.Text) == true)
                {
                    if (CheckPassword(Entry_Password.Text, Entry2_Password.Text) == true)
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            user.Password = GetMd5Hash(md5Hash, user.Password);
                            
                        }

                        await AddItem(user);

                        await Navigation.PushAsync(new UserHome());
                    }
                    else
                    {
                        //make a warning appear on view showing that passwords do not match
                        warrning("password");
                    }
                }else
                {
                    warrning("user");
                }
            }else
                warrning("user");

        }


        private void warrning(string type) {
            //make a warning appear on view showing that passwords do not match
            if (type.Equals("password"))
            {
                Entry_Password.Text = "";
                Entry2_Password.Text = "";
                Lbl_passwordError.IsVisible = true;
            } else if (type.Equals("user"))
            {
                Entry_Username.Text="";
                Entry_Password.Text = "";
                Entry2_Password.Text = "";
                Lbl_passwordError.IsVisible = true;
            }
        }
         
        async Task AddItem(User user)
        {
            await userManger.InsertUser(user);
            /*   user.ItemsSource = await userManger.GetTodoItemsAsync();*/
        }


        private Boolean CheckPassword(String pass1, String pass2)
        {
            if(pass2.Length < 6 && pass2.Length < 6)
            {
                //do something
                //consider doing this after checking passwords are same. Does order matter?
                return false;
            }
            if (pass1.Equals(pass2))
            {
                Lbl_passwordError.IsVisible = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean CheckEmail(String email)
        {

            Regex pattern =  new Regex(@"[a-zA-Z0-9]{1,30}@[a-zA-Z]{1,20}.[a-zA-Z]{1,10}");
            Match match = pattern.Match(email);
            if(match.Success)
            {
                Lbl_emailError.IsVisible = false;
                return true;
            }
            else
            {
                return false;
            }
        }


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

