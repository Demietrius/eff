using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using eff.Models;
using eff.ViewModels;

namespace eff.Views
{
    public partial class CreateUser : ContentPage
    {

        UserManger userManger;
        public CreateUser()
        {
            InitializeComponent();
            userManger = UserManger.DefaultManager;
        }

        public async void InsertUser(object sender, EventArgs e)
        {
            var user = new User { Username = Entry_Username.Text, Password = Entry_Password.Text};

            await AddItem(user);
        }

        async Task AddItem(User user)
        {
            await userManger.InsertUser(user);
            /*   user.ItemsSource = await userManger.GetTodoItemsAsync();*/
        }
    }
}

