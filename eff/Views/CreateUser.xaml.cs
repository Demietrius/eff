using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using eff.Models;
using eff.ViewModels;
using System.Text.RegularExpressions;

namespace eff.Views
{
    public partial class CreateUser : ContentPage
    {

        UserManager userManger;
        public CreateUser()
        {
            InitializeComponent();
            userManger = UserManager.DefaultManager;
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

  
    }
}

