using System;
using System.Collections.Generic;
using System.Text;
using eff.Models;

namespace eff.ViewModels
{
    class HostManager
    {
        static HostManager defaultInstance = new HostManager();


        public static HostManager DefaultManager
       {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
       }


      /*  private async void HostLogic(Rooms Room, User user)
        {
            string LikedPlace;
            Rooms UpdatedRoom = await RoomManager.JoinRoom(Room.RoomNumber, Room.PIN);
            LikedPlace = UpdatedRoom.ListOfResturants;

            var dicationary = LikedPlace.GroupBy(str => str)
                .ToDictionary(group => group.Key, group => group.Count());
            Console.WriteLine(dicationary);

        }
*/


    }

}
