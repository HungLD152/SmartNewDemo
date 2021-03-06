﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SmartNews.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : PopupPage
    {
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList<TabBarItemModel>), typeof(CategoryPage), null, BindingMode.TwoWay);
        public static BindableProperty ItemsSelectedProperty = BindableProperty.Create(nameof(ItemSelected), typeof(object), typeof(CategoryPage), null, BindingMode.TwoWay);
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

        private void Item_OnCategoryItemClicked(object sender, object e)
        {
            var itemCategory = (TabBarItemModel)e;
            PageCategory.Title = itemCategory.TitleBar;
            ControlTabBar control = new ControlTabBar();
            control.AddItem(itemCategory.ListCategory);
        }

        private async void CloseClick(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public CategoryPage()
        {
            InitializeComponent();
        }
    }
}
