using System;
using System.ComponentModel;
using SmartNews.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartNews.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}