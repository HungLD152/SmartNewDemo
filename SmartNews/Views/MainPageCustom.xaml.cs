using System;
using System.Collections.Generic;
using SmartNews.Models;
using SmartNews.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SmartNews.Views
{
    public partial class MainPageCustom : ContentPage
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        RSSFeedItem rssItem;
        double scrollOffet;
        double previousOffset;
        public MainPageCustom()
        {
            InitializeComponent();
            rssItem = new RSSFeedItem();
            var setting = new SettingPage();
            var settingTabbar = new SettingTabView();
            viewModel.Url = "https://cdn.24h.com.vn/upload/rss/trangchu24h.rss";
            viewModel.LoadRssFeed();
            BindingContext = viewModel;
            setting.UpdateStyleItem();
            settingTabbar.UpdateSettingItem();
            viewModel.heightImages = 40;
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("TabItem"))
            {
                if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["TabItem"].ToString()))
                {
                    TabBar.IsVisible = false;
                }
                else
                {
                    TabBar.IsVisible = true;
                }
            }
            TabBar.OnTabBarClicked += TabBar_OnTabItemClicked;
        }

        private void TabBar_OnTabItemClicked(object sender, string e)
        {
            viewModel.Url = e;
            viewModel.LoadRssFeed();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                // Deselect item.
                ((Xamarin.Forms.ListView)sender).SelectedItem = null;

                // Set WebView source to RSS item
                rssItem = (RSSFeedItem)args.SelectedItem;

                // For iOS 9, a NSAppTransportSecurity key was added to 
                //  Info.plist to allow accesses to EarthObservatory.nasa.gov sites.
                //webView.Source = rssItem.Link;

                await Navigation.PushAsync(new RssDetailsPage(rssItem.Link));
                // Hide and make visible.
                //ShowData.IsVisible = false;
                //webLayout.IsVisible = true;
            }
        }

        void OnSearchButtonPressed(object sender, EventArgs args)
        {
            viewModel.LoadRssFeed();
        }


        void OnTextChanged(object sender, EventArgs e)
        {
            viewModel.LoadRssFeed();
        }

        void OnSettingButtonPressed(object sender, EventArgs args)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SettingPage());
        }

        private void Scrollview_Scrolled(object sender, ScrolledEventArgs e)
        {
            var senderObj = sender as Xamarin.Forms.ScrollView;
            double minHeight = 0;
            double maxHeight = 40;
            var scrollY = e.ScrollY;
            bool checkToTop = false;
            //if (scrollY == 0)
            //    return;
            if (previousOffset >= scrollY)
            {
                if (viewModel.heightImages - scrollY >= minHeight && viewModel.heightImages - scrollY <= maxHeight)
                {
                    viewModel.heightImages -= scrollY;
                    //listView.ScrollTo(viewModel.Items[0], ScrollToPosition.Start, true);
                }
            }
            else
            {
                checkToTop = true;
                //Down direction 
                if (viewModel.heightImages - scrollY >= minHeight && viewModel.heightImages - scrollY <= maxHeight)
                {
                    viewModel.heightImages -= scrollY;
                    //senderObj.ScrollToAsync(viewModel.Items[0], ScrollToPosition.Start, true);
                }
            }
            if (checkToTop)
            {
                checkToTop = false;
                //senderObj.ScrollToAsync(viewModel.Items[0], ScrollToPosition.Start, true);
            }
            previousOffset = scrollY;
        }
    }
}

