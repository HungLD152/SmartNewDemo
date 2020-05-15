using System;
using System.Collections.Generic;
using SmartNews.Models;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class CategoryView : StackLayout
    {
        public event EventHandler<object> OnCategoryItemClicked;
        public CategoryView()
        {
            InitializeComponent();
        }
        private void OnCategoryTapped(object sender, EventArgs e)
        {
            OnCategoryItemClicked?.Invoke(this, (BindingContext as TabBarItemModel));
        }
    }
}
