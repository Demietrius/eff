using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using eff.Services;
namespace eff.ViewModels
{
    public class GroupPageModel : BaseViewModel
    {
        public GroupPageModel()
        {
            Title = "DataBase";
            
           
        }
        Database_connection Connection;

        
        public string Connect()
        {
            Connection = new Database_connection();
            string message = Connection.TestDatabaseConnection();

            return message;
            
        }
    }
}