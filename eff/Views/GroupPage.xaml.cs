using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eff.Models;
using eff.Views;
using eff.ViewModels;

namespace eff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        GroupPageModel viewModel;
        public GroupPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new GroupPageModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string message = viewModel.Connect();

        }
    }
}