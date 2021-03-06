﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace eff.Models
{
    public class Rooms
    {
        public string RoomName { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
        public string PIN { get; set; }
        public string RoomNumber { get; set; }
        public string Hoste { get; set; }
        public string Date { get; set; }
        public string GameRound { get; set; }
        public string GroupId { get; set; }
        public string Location { get; set; }
        public string Distance { get; set; }
        public string NumberOfResturants { get; set; } 
        public string Price { get; set; }
        public string City { get; set; }
        public string TypeOfFood { get; set; }
        public string UserId { get; set; }
        public List<User> ListOfUsers { get; set; }
        public List<string> ListOfResturants { get; set; }
        public bool StartGame { get; set; }
        public string RoundTime { get; set; }

    }
}
