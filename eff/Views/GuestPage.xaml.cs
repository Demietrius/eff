using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eff.Views
{
    public partial class GuestPage : ContentPage
    {
        public GuestPage()
        {
            InitializeComponent();
            if ((bool)(Application.Current.Properties["IsLoggedIn"] = true)) {
                Lbl_tempName.IsVisible = false; 
                Entry_tempName.IsVisible = false;
            }
        }

    }
}
