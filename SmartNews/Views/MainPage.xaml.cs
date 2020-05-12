using System;
using System.ComponentModel;
using SmartNews.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartNews.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
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