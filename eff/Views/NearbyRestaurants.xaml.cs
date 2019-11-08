using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Collections.ObjectModel;
using eff.Models;
using Newtonsoft.Json.Linq;
using eff.ViewModels;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyRestaurants : ContentPage
    {
        YelpManager yelpManager;
        public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
        public NearbyRestaurants()
        {
            InitializeComponent();
            PlacesView.ItemsSource = Places;
            yelpManager = new YelpManager();
        }

        protected async void RequestPlaces_Clicked(object sender, EventArgs e)
        {
            yelpManager.RequestPlaces_Clicked();
            
   
        }


    }

}