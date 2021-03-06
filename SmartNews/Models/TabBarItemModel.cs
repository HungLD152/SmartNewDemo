﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartNews.Models
{
    public class TabBarItemModel : BaseModel
    {
        public string TitleBar { get; set; }
        public string Url { get; set; }
        public string UrlImages { get; set; }
        public string ColorName { get; set; }
        public Color ItemColor { get; set; }
        public bool IsSelected { get; set; }
        public int NotificationNumber { get; set; }
        public int TabPosition { get; set; }

        public List<TabBarItemModel> ListCategory { get; set; }
    }   
}
