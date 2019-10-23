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
    class GroupManager
    {
        static GroupManager defaultInstance = new GroupManager();

        const string accountURL = @"https://effdatabase.documents.azure.com:443/";
        const string accountKey = @"0LhL8FvWxysH8SLdx0GUlD2OLlghxX1jJcYAPVceYBgi32ocmNwvbQJHJlthaSy5eBKEH6uXIwTyrEEFChDWJA==";
        const string databaseId = @"user";
        const string collectionId = @"GroupManager";

        private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private DocumentClient client;
        
        private GroupManager()
        {
            client = new DocumentClient(new System.Uri(accountURL), accountKey);
        }
    }
}
