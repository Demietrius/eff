﻿using System;
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



    }
}
