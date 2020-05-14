﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class CategoryView : StackLayout
    {
        public event EventHandler<string> OnCategoryItemClicked;
        public CategoryView()
        {
            InitializeComponent();
        }
        private void OnCategoryTapped(object sender, EventArgs e)
        {
            OnCategoryItemClicked?.Invoke(this, (BindingContext as TabBarItemModel).TitleBar);
        }
    }
}
