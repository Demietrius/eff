using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using eff.Models;
using Microsoft.Azure.Documents;
using User = eff.Models.User;

namespace eff.ViewModels
{
    class UserManager
    {


        static UserManager defaultInstance = new UserManager();

        const string accountURL = @"https://effdatabase.documents.azure.com:443/";
        const string accountKey = @"0LhL8FvWxysH8SLdx0GUlD2OLlghxX1jJcYAPVceYBgi32ocmNwvbQJHJlthaSy5eBKEH6uXIwTyrEEFChDWJA==";
        const string databaseId = @"user";
        const string collectionId = @"clients";

        private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private DocumentClient client;

        public List<User> list
        {
            get;
            private set;
        }


        private UserManager()
        {
            client = new DocumentClient(new System.Uri(accountURL), accountKey);
        }

        public static UserManager DefaultManager
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

        public List<User> users { get; private set; }



        public async Task<List<User>> GetUserById(int UserId)
        {
            try
            {
                // The query excludes completed TodoItems
                var query = client.CreateDocumentQuery<User>(collectionLink, new FeedOptions { MaxItemCount = -1 })
                      .Where(user => user.Id.Equals(UserId))
                      .AsDocumentQuery();

                users = new List<User>();
                while (query.HasMoreResults)
                {
                    users.AddRange(await query.ExecuteNextAsync<User>());
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
                return null;
            }
            return users;
        }


        public List<User> Clients { get; private set; }

        public async Task<List<User>> IsExsitingUser(User NewUser)
        {
            try
            {

                var query = client.CreateDocumentQuery<User>(collectionLink, new FeedOptions { MaxItemCount = -1 })
                       .Where(user => user.Username =="asdf@aol.com")
                       .AsDocumentQuery();

                Clients = new List<User>();
                while (query.HasMoreResults)
                {
                    Clients.AddRange(await query.ExecuteNextAsync<User>());
                
                }

            }
catch (Exception e)
            {
                Console.Error.WriteLine(@"Error{0}", e.Message);
                return null;
            }

            return Clients;
        }


        public async Task<User> InsertUser(User user)
        {
            var List = await IsExsitingUser(user);

            try
            {

                var result = await client.CreateDocumentAsync(collectionLink, user);
                user.Id = result.Resource.Id;
                users.Add(user);


            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
            }
            return user;
        }

         
        /*public async Task CompleteItemAsync(User user)
        {
            try
            {
                item.Complete = true;
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, item.Id), item);

                Items.Remove(item);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
            }*/

    }
}
