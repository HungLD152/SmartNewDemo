using System;
using Xamarin.Forms;

namespace SmartNews.Models
{
    public class TabBarItemModel
    {
        public string TitleBar { get; set; }
        public string Url { get; set; }
        public string UrlImages { get; set; }
        public Color ItemColor { get; set; }
        public bool IsSelected { get; set; }
    }   
}
