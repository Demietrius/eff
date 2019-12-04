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

        const string accountURL = @"https://effdb.documents.azure.com:443/";
        const string accountKey = @"zh7XHYblEgYk66t9ytPuYgJTBZJ7wOoCDIq1nQPrP6nUxPGwwfIVH1N3etlEgrMJpIMLU34B7Un8qFuXIJAt5w==";
        const string databaseId = @"eff";
        const string collectionId = @"GroupManager";

        private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private DocumentClient client;
        
        private GroupManager()
        {
            client = new DocumentClient(new System.Uri(accountURL), accountKey);
        }
    }
}
