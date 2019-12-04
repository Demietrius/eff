using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using eff.Models;
namespace eff.ViewModels
{
    class RoomManager
    {
        static RoomManager defaultInstance = new RoomManager();


        const string accountURL = @"https://effdb.documents.azure.com:443/";
        const string accountKey = @"zh7XHYblEgYk66t9ytPuYgJTBZJ7wOoCDIq1nQPrP6nUxPGwwfIVH1N3etlEgrMJpIMLU34B7Un8qFuXIJAt5w==";
        const string databaseId = @"eff";
        const string collectionId = @"Room";

        private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private DocumentClient room;

        private RoomManager()
        {
            room = new DocumentClient(new System.Uri(accountURL), accountKey);
        }



        public static RoomManager DefaultManager
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


        public List<Rooms> Rooms { get; private set; }



        public async Task<Rooms> GetById(string RoomNumber)
        {
            try
            {
                // The query excludes completed TodoItems
                var query = room.CreateDocumentQuery<Rooms>(collectionLink, new FeedOptions { MaxItemCount = -1 })
                      .Where(room => room.RoomNumber ==RoomNumber)
                      .AsDocumentQuery();

                Rooms = new List<Rooms>();
                while (query.HasMoreResults)
                {
                    Rooms.AddRange(await query.ExecuteNextAsync<Rooms>());
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
                return null;
            }
            return Rooms[0];
        }



        public async Task<List<User>> JoindUsers(string RoomId)
        {
            try
            {
                var Rooms = await GetById(RoomId);
                List<User> users = Rooms.ListOfUsers;
                return users;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
                return null;
            }
        }




        public async Task<Rooms> InsertRoom(Rooms NewRoom)
        {
            try
            {
                Rooms = new List<Rooms>();
                var result = await room.CreateDocumentAsync(collectionLink, NewRoom);
                NewRoom.ID = result.Resource.Id;
                Rooms.Add(NewRoom);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
            }
            return NewRoom;
        }



        public async Task<Rooms> JoinRoom(string RoomNumber, string pin)
        {
            try
            {
                // The query excludes completed TodoItems
                var query = room.CreateDocumentQuery<Rooms>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true, MaxItemCount = -1 })
                      .Where(room => room.RoomNumber == RoomNumber && room.PIN == pin)
                      .AsDocumentQuery();

                Rooms = new List<Rooms>();
                while (query.HasMoreResults)
                {
                    Rooms.AddRange(await query.ExecuteNextAsync<Rooms>());
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
                return null;
            }
            return Rooms[0];   
        }



        public async Task JoindUsers(User user, Rooms JoinedRoom)
        {
            try
            {
                var TempRoom = await JoinRoom(JoinedRoom.RoomNumber, JoinedRoom.PIN);
                User TempUser = new User() { Username = user.Username, Email = user.Email, Id = user.Id };

                if (TempRoom.ListOfUsers == null)
                    TempRoom.ListOfUsers = new List<User>() { TempUser };
                else
                    TempRoom.ListOfUsers.Add(TempUser);

                await room.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, TempRoom.ID), TempRoom);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
            }
        }


       public async Task StartGame(Rooms Room)
       {
            try
            {
                DateTime Now = DateTime.Now;
                Room.StartGame = true;
                Room.Date = Now.ToString();
                await room.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, Room.ID), Room);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR fuckfuckfuckfuck {0}", e.Message);
            }
        }

        public async Task<Rooms> GetGameStatus(Rooms Room)
        {
            try
            {
                // The query excludes completed TodoItems
                var query = room.CreateDocumentQuery<Rooms>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true, MaxItemCount = -1 })
                      .Where(room => room.RoomNumber == room.RoomNumber)
                      .AsDocumentQuery();

                Rooms = new List<Rooms>();
                while (query.HasMoreResults)
                {
                    Rooms.AddRange(await query.ExecuteNextAsync<Rooms>());
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
                return null;
            }
            return Rooms[0];
        }

        public async Task<List<User>> GetUsersInRoom(Rooms Room)
        {
            List<User> User = new List<User>();
            Room = await JoinRoom(Room.RoomNumber, Room.PIN);

            User = Room.ListOfUsers;
            return User;
            
        }
    }


}
