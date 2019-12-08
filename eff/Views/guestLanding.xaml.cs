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
        List<string> LikedPlace;
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
            if (user.IsHost)
               // HostLogic();
            InitializeComponent();

            int round;
            while (Room.GameRound != null)
            {
                Room = await RoomManager.JoinRoom(Room.RoomNumber, Room.PIN);  // get room
                if (Room.StartGame == true)
                    await Navigation.PushAsync(new GetPlaces(user, Room));
                System.Threading.Thread.Sleep(1000);
            }

            while (Room.StartGame == false)
            {
                Room = await RoomManager.JoinRoom(Room.RoomNumber, Room.PIN);
                string tempround = Application.Current.Properties["GameRound"].ToString();
                if (Room.GameRound == tempround)
                    await Navigation.PushAsync(new GetPlaces(user, Room)); // get room

                System.Threading.Thread.Sleep(1000);
            }
        }
        //TODO:  tyring to make logic for the host ( user.is host is set at initiate game under the start function)  I moved the below function to HostManger in viewmodels
/*
        private async void HostLogic()
        {
            Rooms UpdatedRoom = await RoomManager.JoinRoom(Room.RoomNumber, Room.PIN);
            LikedPlace = UpdatedRoom.ListOfResturants;

            var dicationary = LikedPlace.GroupBy(str => str)
                .ToDictionary(group => group.Key, group => group.Count());
            Console.WriteLine(dicationary);

        }*/
       

    }
}