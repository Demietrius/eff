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


        const string accountURL = @"https://effdatabase.documents.azure.com:443/";
        const string accountKey = @"0LhL8FvWxysH8SLdx0GUlD2OLlghxX1jJcYAPVceYBgi32ocmNwvbQJHJlthaSy5eBKEH6uXIwTyrEEFChDWJA==";
        const string databaseId = @"user";
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



        public async Task<List<Rooms>> GetById(int RoomId)
        {
            try
            {
                // The query excludes completed TodoItems
                var query = room.CreateDocumentQuery<Rooms>(collectionLink, new FeedOptions { MaxItemCount = -1 })
                      .Where(room => room.ID.Equals(RoomId))
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
            return Rooms;
        }



        public async Task<List<User>> JoindUsers(int RoomId)
        {
            try
            {
                var Rooms = await GetById(RoomId);
                List<User> users = Rooms[0].ListOfUsers;
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



        public async Task<List<Rooms>> JoinRoom(string RoomNumber, string pin)
        {
            try
            {
                // The query excludes completed TodoItems
                var query = room.CreateDocumentQuery<Rooms>(collectionLink, new FeedOptions { MaxItemCount = -1 })
                      .Where(room => room.RoomNumber.Equals(RoomNumber) && room.PIN.Equals(pin))
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
            return Rooms;   
        }



        public async Task JoindUsers(User user, Rooms JoinedRoom)
        {
            try
            {
                var TempUser = new User() { Username = user.Username, Email = user.Email, Id = user.Id };
                JoinedRoom.ListOfUsers.Add(user);

                await room.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, JoinedRoom.ID), JoinedRoom);

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
                Room.StartGame = true;
                await room.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, Room.ID), Room);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
            }
        }
    


    }
}
