using System.ComponentModel;
using SmartNews.ViewModels;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace SmartNews.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
        }
    }
}