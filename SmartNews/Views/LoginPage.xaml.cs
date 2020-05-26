using SmartNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel viewModel = new LoginPageViewModel();
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.Password != string.Empty)
            {
                viewModel.IsPassword = true;
                viewModel.IsVisiableShowPass = true;
                viewModel.IsVisiableHiddenPass = false;
            }
            else
            {
                viewModel.IsPassword = true;
                viewModel.IsVisiableShowPass = false;
                viewModel.IsVisiableHiddenPass = false;
            }
        }

        private async void HandleChecked(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.IsChecked)
            {
                try
                {
                    await SecureStorage.SetAsync("email", viewModel.Email);
                    await SecureStorage.SetAsync("password", viewModel.Password);
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.
                }
            }
        }
    }
}