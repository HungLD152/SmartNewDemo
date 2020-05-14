using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SmartNews.Models;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class CategoryPage : ContentPage
    {
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList<TabBarItemModel>), typeof(ControlTabBar), null, BindingMode.TwoWay);
        public static BindableProperty ItemsSelectedProperty = BindableProperty.Create(nameof(ItemSelected), typeof(object), typeof(ControlTabBar), null, BindingMode.TwoWay);
        public IList<TabBarItemModel> ItemsSource
        {
            get { return (IList<TabBarItemModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public object ItemSelected
        {
            get { return GetValue(ItemsSelectedProperty); }
            set { SetValue(ItemsSelectedProperty, value); }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                ContainerLeft.Children.Clear();
                ContainerRight.Children.Clear();
                var ItemSourceLeft = ItemsSource.Take(5).ToList();
                var ItemSourceRight = ItemsSource.Skip(5).Take(5).ToList();
                if (ItemsSource?.Count > 0)
                    foreach (var dataleft in ItemSourceLeft)
                    {
                        var item = new CategoryView { BindingContext = dataleft };
                        item.OnCategoryItemClicked += Item_OnCategoryItemClicked;
                        ContainerLeft.Children.Add(item);
                    }
                    foreach (var dataright in ItemSourceRight)
                    {
                        var item = new CategoryView { BindingContext = dataright };
                        item.OnCategoryItemClicked += Item_OnCategoryItemClicked;
                        ContainerRight.Children.Add(item);
                    }
            }
            if (propertyName == ItemsSelectedProperty.PropertyName)
            {

            }
        }
        private void Item_OnCategoryItemClicked(object sender, string e)
        {
            PageCategory.Title = e.ToString();
            //Console.WriteLine("Item_OnCategoryItemClicked: " + e.ToString());
            //var senderObj = (CategoryView)sender;
            //foreach (var itemLeft in ContainerLeft.Children)
            //{
            //    (itemLeft.BindingContext as TabBarItemModel).IsSelected = false;
            //    senderObj.BackgroundColor = Color.White;
            //}
            //foreach (var itemRight in ContainerRight.Children)
            //{
            //    (itemRight.BindingContext as TabBarItemModel).IsSelected = false;
            //    senderObj.BackgroundColor = Color.White;
            //}
            //(senderObj.BindingContext as TabBarItemModel).IsSelected = true;
            //if ((senderObj.BindingContext as TabBarItemModel).IsSelected)
            //{
            //    senderObj.BackgroundColor = Color.Red;
            //}
        }
        public CategoryPage()
        {
            InitializeComponent();
        }
    }
}
