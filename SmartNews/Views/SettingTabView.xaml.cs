﻿using SmartNews.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingTabView : ContentPage
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        double scrollOffet;
        double previousOffset;

        public SettingTabView()
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.heightImages = 150;
            listView.BindingContextChanged += (sender, e) =>
            {
                var change = e;
            };
            Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>
            {
                CarouselViewer.Position = (CarouselViewer.Position + 1) % viewModel.ItemCategory.Count;

                return true;
            }));
        }

        private void Scrollview_Scrolled(object sender, ScrolledEventArgs e)
        {
            double minHeight = 50;
            double maxHeight = 150;
            var scrollY = e.ScrollY;
            bool checkToTop = false;
            if (scrollY == 0)
                return;
            if (previousOffset >= scrollY)
            {
                if (viewModel.heightImages - scrollY >= minHeight && viewModel.heightImages - scrollY <= maxHeight)
                {
                    viewModel.heightImages -= scrollY * 0.75;
                    //listView.ScrollTo(viewModel.ItemTabBar[0], Syncfusion.ListView.XForms.ScrollToPosition.Start, true);
                }
            }
            else
            {
                checkToTop = true;
                //Down direction 
                if (viewModel.heightImages - scrollY >= minHeight && viewModel.heightImages - scrollY <= maxHeight)
                {
                    viewModel.heightImages -= scrollY * 0.75;
                }
                //int index = listView.DataSource.DisplayItems.IndexOf(viewModel.ItemTabBar[0]);
            }
            if (checkToTop)
            {
                checkToTop = false;
                //listView.ScrollTo(viewModel.ItemTabBar[0], Syncfusion.ListView.XForms.ScrollToPosition.Start, true);
            }
            previousOffset = scrollY;
        }

        //public void UpdateSettingItem()
        //{
        //    if (Application.Current.Properties.ContainsKey("TabItem"))
        //    {
        //        showTabItem.IsToggled = Convert.ToBoolean(Application.Current.Properties["TabItem"].ToString());
        //    }
        //}

        void OnCheckedClick(object sender, ToggledEventArgs e)
        {
            //Applicatisson.Current.Properties["TabItem"] = showTabItem.IsToggled;
            //Application.Current.SavePropertiesAsync();
        }

        void Edit_Clicked(object sender, EventArgs e)
        {

        }

        void Update_Clicked(object sender, EventArgs e)
        {

        }
    }
}