using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eff.ViewModels;
using eff.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class guestLanding : ContentPage
    {
        RoomManager RoomManager;
        Rooms Room;
        User user;
        public guestLanding(Rooms room, User user)
        {
            InitializeComponent();
            RoomManager = RoomManager.DefaultManager;
            this.Room = room;
            this.user = user;

            GetGameStatusAsync();
        }

        private async Task GetGameStatusAsync()
        {
            while (Room.StartGame == false)
            {
                Room = await RoomManager.JoinRoom(Room.RoomNumber, Room.PIN);
                if(Room.StartGame == true)
                    await Navigation.PushAsync(new GetPlaces(user, Room));
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}