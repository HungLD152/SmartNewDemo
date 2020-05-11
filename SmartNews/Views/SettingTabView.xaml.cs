﻿using SmartNews.ViewModels;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
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
        VisualContainer container;
        ExtendedScrollView scrollview;
        double scrollOffet;
        double previousOffset;

        public SettingTabView()
        {
            InitializeComponent();
            BindingContext = viewModel;
            UpdateSettingItem();
            viewModel.heightImages = 150;
            this.listView.DragDropController.UpdateSource = true;
            container = listView.GetVisualContainer();
            scrollview = listView.GetScrollView();
            scrollview.Scrolled += Scrollview_Scrolled;
        }

        private void Scrollview_Scrolled(object sender, ScrolledEventArgs e)
        {
            scrollOffet = (double)container.GetType().GetRuntimeProperties().First(p => p.Name == "ScrollOffset").GetValue(container);
            double minHeight = 50;
            double maxHeight = 150;
            var scrollY = e.ScrollY;
            if (e.ScrollY == 0)
                return;
            if (previousOffset >= e.ScrollY)
            {
                if (viewModel.heightImages - scrollY >= minHeight && viewModel.heightImages - scrollY <= maxHeight)
                {
                    viewModel.heightImages = 150 - e.ScrollY;
                }
                try
                {
                    var firstItem = listView.DataSource.DisplayItems.FirstOrDefault();
                    if (firstItem != null)
                    {
                        scrollview.ScrollToAsync(listView, Xamarin.Forms.ScrollToPosition.Start, true);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            else
            {
                //Down direction 
                if (viewModel.heightImages - scrollY >= minHeight && viewModel.heightImages - scrollY <= maxHeight)
                {
                    viewModel.heightImages = (150 - scrollY) > minHeight ? (150 - scrollY) : minHeight;
                }

                //int index = listView.DataSource.DisplayItems.IndexOf(viewModel.ItemTabBar[0]);
                //scrollview.ScrollToAsync(index, Xamarin.Forms.ScrollToPosition.Start, true);
                try
                {
                    //scrollview.ScrollToAsync(0, sizeToUp, false);
                    var firstItem = listView.DataSource.DisplayItems.FirstOrDefault();
                    if (firstItem != null)
                    {
                        scrollview.ScrollToAsync(listView, Xamarin.Forms.ScrollToPosition.Start, true);
                        //listView.ScrollTo(firstItem, Syncfusion.ListView.XForms.ScrollToPosition.Start, false);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            Console.WriteLine("viewModel.heightImages - e.ScrollY : " + (viewModel.heightImages - scrollY));
            Console.WriteLine("scrollOffet: " + scrollOffet);
            Console.WriteLine("ScrollY: " + scrollY);
            previousOffset = e.ScrollY;
        }

        private void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Drop)
            {
                viewModel.ItemTabBar.MoveTo(1, 5);
            }
        }

        public void UpdateSettingItem()
        {
            if (Application.Current.Properties.ContainsKey("TabItem"))
            {
                ShowTabItem.IsToggled = Convert.ToBoolean(Application.Current.Properties["TabItem"].ToString());
            }
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties["TabItem"] = ShowTabItem.IsToggled;
            Application.Current.SavePropertiesAsync();
        }
    }
}