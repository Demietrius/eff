using System;
using System.Collections.Generic;
using eff.Models;
using Xamarin.Forms;
using eff.ViewModels;
namespace eff.Views
{
    public partial class GuestPage : ContentPage
    {
        RoomManager roomManger;
        User user;
        public GuestPage(User user)
        {
            this.user = user;
            roomManger = RoomManager.DefaultManager;
            InitializeComponent();
            if (user.IsGuest == false)
            {
                Lbl_tempName.IsVisible = false;
                Entry_tempName.IsVisible = false;
            }
          //  user.Username == Entry_tempName;
        }
        public GuestPage(string id)
        {
            InitializeComponent();
            roomManger = RoomManager.DefaultManager;
            
        }

        public async void Join (Object sender, EventArgs e)
        {
            if (user.IsGuest == true)
            {
                user.Username = Entry_tempName.Text;
                Rooms room = new Rooms() { RoomNumber = Entry_Lobby.Text, PIN = Entry_PIN.Text };
                await roomManger.JoindUsers(user, room);
                await Navigation.PushAsync(new guestLanding(room, user));
            }
        }

    }
}
