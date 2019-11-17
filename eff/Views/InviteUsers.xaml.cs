using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eff.Views
{
    public partial class InviteUsers : ContentPage
    {
        public InviteUsers()
        {
            InitializeComponent();
        }

        protected async void AddUser(object sender, EventArgs e)
        {
            var user = Btn_addmore.Text;

        }
    }
}
