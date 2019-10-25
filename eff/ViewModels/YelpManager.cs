using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Yelp;
using eff.Models;
using Yelp.Api;
namespace eff.ViewModels
{
    class YelpManager
    {
        private const string BASE_ADDRESS = "https://api.yelp.com/v3/businesses/search?";

        const string client_id = "QuruhD41QSLsLtYtnLghSQ";
        const string client_key = "SQNXg8He6dvuTfcApGfUhCyof9oPKoLxXDMmBUXf_F8LLtHHLH7jh4UYpBFJIsNDkyQlL0cujbzxkjvXJWogh48hBBwIo6tLiEt-LvD-UnQTUKUTn0jVWQyVM2-yXXYx";


        Client client = new Client(client_key, null);

    }
    public async Task<YelpObject> SearchBusinessesAllAsync(string term, double latitude, double longitude, CancellationToken ct = default(CancellationToken))
    {
        SearchRequest search = new SearchRequest();
        if (!string.IsNullOrEmpty(term))
            search.Term = term;
        search.Latitude = latitude;
        search.Longitude = longitude;
        return this.SearchBusinessesAllAsync(search, ct);
    }
}
