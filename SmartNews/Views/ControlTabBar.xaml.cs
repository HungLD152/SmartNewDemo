using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SmartNews.Models;
using SmartNews.ViewModels;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class ControlTabBar : ScrollView
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        private TabBarItemModel selectedItem;
        public TabBarItemModel SelectedItem { get { return selectedItem; } set { selectedItem = value; OnPropertyChanged("SelectedItem"); } }
        public event EventHandler<string> OnTabBarClicked;
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

        public ControlTabBar()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                Container.Children.Clear();
                if (ItemsSource?.Count > 0)
                    foreach (var data in ItemsSource)
                    {
                        var item = new TabItem { BindingContext = data };
                        item.OnTabItemClicked += Item_OnTabItemClicked;
                        Container.Children.Add(item);
                    }
            }
            if (propertyName == ItemsSelectedProperty.PropertyName)
            {

            }
        }

        public void AddItem(List<TabBarItemModel> lstItems)
        {
            viewModel.CheckMenuItem = false;
            Container.Children.Clear();
            if (lstItems?.Count > 0)
                foreach (var data in lstItems)
                {
                    var item = new TabItem { BindingContext = data };
                    item.OnTabItemClicked += Item_OnTabItemClicked;
                    Container.Children.Add(item);
                }
        }

        private void Item_OnTabItemClicked(object sender, string e)
        {
            var senderObj = (TabItem)sender;
            foreach (var item in Container.Children)
            {
                (item.BindingContext as TabBarItemModel).IsSelected = false;
                item.Margin = new Thickness(0, 10, 0, 0);
            }
            (senderObj.BindingContext as TabBarItemModel).IsSelected = true;
            if ((senderObj.BindingContext as TabBarItemModel).IsSelected)
            {
                senderObj.Margin = new Thickness(0, 5, 0, 0);
                senderObj.Padding = new Thickness(0, 0, 0, -5);
                //senderObj.HeightRequest = 50;
                BottomColor.BackgroundColor = (senderObj.BindingContext as TabBarItemModel).ItemColor;
                BottomColor.Margin = new Thickness(0, -4, 0, 4);
            }
            //scroll position
            bool animate = true;
            ScrollBar.ScrollToAsync(senderObj, ScrollToPosition.Center, animate);
            OnTabBarClicked?.Invoke(this, e);
        }
    }
}
