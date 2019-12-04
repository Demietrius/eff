using System;
using System.Collections.Generic;
using eff.Models;
using Xamarin.Forms;

namespace eff.Views
{
    public partial class GuestPage : ContentPage
    {

        public GuestPage(User user)
        {
            InitializeComponent();
            Lbl_tempName.IsVisible = false;
            Entry_tempName.IsVisible = false;
        }
        public GuestPage(string id)
        {
            InitializeComponent();
            
        }

    }
}
