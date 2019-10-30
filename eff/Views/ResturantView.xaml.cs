using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eff.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResturantView : ContentPage
    {
        public ResturantView()
        {
            ResturantView resturantView = new ResturantView();
            InitializeComponent();
            BindingContext = new ResturantViewTest();
        }
    }
}