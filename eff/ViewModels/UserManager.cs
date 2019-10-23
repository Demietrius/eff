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
    class UserManager
    {
        static UserManager defaultInstance = new UserManager();

        const string accountURL = @"https://effdatabase.documents.azure.com:443/";
        const string accountKey = @"0LhL8FvWxysH8SLdx0GUlD2OLlghxX1jJcYAPVceYBgi32ocmNwvbQJHJlthaSy5eBKEH6uXIwTyrEEFChDWJA==";
        const string databaseId = @"user";
        const string collectionId = @"clients";

        private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private DocumentClient client;

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

       /* private bool IsExsitingUser(User NewUser)
        {
            try
            {
                var query = client.CreateDocumentQuery<User>(collectionLink, new FeedOptions { MaxItemCount = -1 })
                       .Where(user => user.Username.Equals(NewUser.Username))
                       .AsDocumentQuery();
                if (query.HasMoreResults)
                    return false;
                else
                    return true;

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"Error{0}", e.Message);
            }

            return true;
        }*/


        public async Task<User> InsertUser(User user)
        {
          /*  if (!IsExsitingUser(user))
            {
                return null;
            }*/

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
