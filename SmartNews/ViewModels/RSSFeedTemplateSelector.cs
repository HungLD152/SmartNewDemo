using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmartNews.Models;
using Xamarin.Forms;

namespace SmartNews.ViewModels
{
    public class RSSFeedTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }

        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ObservableCollection<RSSFeedItem>)((ListView)container).ItemsSource).IndexOf(item as RSSFeedItem) % 2 == 0 ? InvalidTemplate : ValidTemplate;
            //if (item is RSSFeedItem)
            //{
            //    var data = item as RSSFeedItem;
            //    if (!string.IsNullOrEmpty(data.Thumbnail))
            //        return InvalidTemplate;
            //}
            //return ValidTemplate;
        }
    }
}
