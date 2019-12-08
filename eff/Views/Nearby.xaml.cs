using System;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using eff.ViewModels;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Nearby : ContentPage
    {
        YelpManager yelpManager;
        public Nearby()
        {
            InitializeComponent();
            yelpManager = YelpManager.DefaultManager;
            yelpManager.Places.Clear();
            ViewPlaces.ItemsSource = yelpManager.Places;
            yelpManager.LikeCount = 0;
            yelpManager.NumberOfPlaces = 0;
            _ = RequestPlaces_ClickedAsync();
        }
        protected async System.Threading.Tasks.Task RequestPlaces_ClickedAsync()
        {
            String location = await yelpManager.GetUserLocationAsync();
            var strlist = location.Split(',');
            String latitude = strlist[0];
            String longitude = strlist[1];
            int radius = 1610;
            int maxresults = 20;
            int price = 4;

            var searchString = yelpManager.GenerateYelpSearchString(latitude, longitude, radius, maxresults, price);

            JObject joResponse = yelpManager.YelpWebRequest(searchString);

            yelpManager.ParseJObjectResponse(joResponse);

        }
    }
}
