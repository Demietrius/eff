using eff.Models;
using System;
using System.Collections.Generic;
using eff.ViewModels;

using Xamarin.Forms;

namespace eff.Views
{
    public partial class GuestPage : ContentPage
    {
        User user;
            RoomManager RoomManager;
        public GuestPage(User user)
        {

            InitializeComponent();
            RoomManager = RoomManager.DefaultManager;

            if ((bool)(Application.Current.Properties["IsLoggedIn"] = true))
            {
                Lbl_GuestID.IsVisible = false;
                Lbl_GuestIDdisplay.IsVisible = false;
            }
        }

        public GuestPage()
        {
            InitializeComponent();
            RoomManager = RoomManager.DefaultManager;
        }

            private async void Join(object sender, EventArgs e)
            {
                List<User> list = new List<User>();
                list.Add(user);

                var Room = await RoomManager.JoinRoom(Entry_Lobby.Text, Entry_PIN.Text);

                await RoomManager.JoindUsers(user,Room[0]);
            }
        

    }
}
