using System;
using System.Collections.Generic;
using System.Text;

namespace eff.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Datbase
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
