using System;
using System.Collections.Generic;
using System.Text;

namespace eff.Models
{
    class Rooms
    {
        public string RoomName { get; set; }
        public string id { get; set; }
        public string RoomNumber { get; set; }
        public string Date { get; set; }
        public string GroupId { get; set; }
        public string Location { get; set; }
        public string Distance { get; set; }
        public string NumberOfResturants { get; set; }
        public string Price { get; set; }
        public string City { get; set; }
        public string TypeOfFood { get; set; }
        public string UserId { get; set; }
        public List<string> ListOfUsers { get; set; }


    }
}
