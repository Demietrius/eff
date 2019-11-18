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
            ViewPlaces.ItemsSource = yelpManager.Places;
            yelpManager.LikeCount = 0;
            yelpManager.NumberOfPlaces = 0;
            RequestPlaces_Clicked();
        }
        protected void RequestPlaces_Clicked()
        {
            var location = yelpManager.GetUserLocationAsync();
            String latitude = location.Result.Item1;
            String longitude = location.Result.Item2;
            int radius = 1610;

            var searchString = yelpManager.GenerateYelpSearchString(latitude, longitude, radius);

            JObject joResponse = yelpManager.YelpWebRequest(searchString);

            yelpManager.ParseJObjectResponse(joResponse);

        }
    }
}
