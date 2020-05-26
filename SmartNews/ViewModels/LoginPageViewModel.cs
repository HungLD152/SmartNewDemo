using SmartNews.Utils.Constants;
using SmartNews.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartNews.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region properties, variable
        public string BtnName { get; set; }
        public bool FrmIsVisiable { get; set; }
        public string AlertMessage { get; set; }
        public bool Busy { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsVisiableShowPass { get; set; }
        public bool IsPassword { get; set; }
        public bool IsChecked { get; set; }
        public bool IsVisiableHiddenPass { get; set; }
        public List<string> ListIconOauth2 { get; set; }
        #endregion

        #region command
        public ICommand FacebookCommand { get; set; }
        public ICommand GoogleCommand { get; set; }
        public ICommand MicrosoftCommand { get; set; }
        public ICommand TelegramCommand { get; set; }
        public ICommand TwitterCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand HiddenPasswordCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand NavigationRegisterCommand { get; set; }
        #endregion

        public LoginPageViewModel()
        {
            FacebookCommand = new Command(async () => await HandleLoginSocial("Facebook"));
            GoogleCommand = new Command(async () => await HandleLoginSocial("Google"));
            MicrosoftCommand = new Command(async () => await HandleLoginSocial("Microsoft"));
            TelegramCommand = new Command(async () => await HandleLoginSocial("Telegram"));
            TwitterCommand = new Command(async () => await HandleLoginSocial("Twitter"));
            NavigationRegisterCommand = new Command((async) =>
            {
                HandleRegisterForm();
            });
            ShowPasswordCommand = new Command(HandleShowPassword);
            HiddenPasswordCommand = new Command(HandleHiddenPassword);

            SubmitCommand = new Command((async) =>
            {
                HandleSubmit();
            });
        }

        private void HandleShowPassword()
        {
            IsPassword = false;
            IsVisiableHiddenPass = true;
            IsVisiableShowPass = false;
        }

        private void HandleHiddenPassword()
        {
            IsPassword = true;
            IsVisiableShowPass = true;
            IsVisiableHiddenPass = false;
        }

        private async void HandleRegisterForm()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private async void HandleSubmit()
        {
            var current = Connectivity.NetworkAccess;
            var emailPattern = @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            try
            {
                if (current != NetworkAccess.Internet)
                {
                    FrmIsVisiable = true;
                    AlertMessage = ConstantMessage.ErrorNetwork;
                    await Task.Delay(3000);
                    FrmIsVisiable = false;
                }
                else if (Email == null || Password == null)
                {
                    FrmIsVisiable = true;
                    AlertMessage = ConstantMessage.EmptyUserOrPass;
                    await Task.Delay(3000);
                    FrmIsVisiable = false;
                }
                else if (!Regex.IsMatch(Email, emailPattern))
                {
                    FrmIsVisiable = true;
                    AlertMessage = ConstantMessage.WrongEmailFormat;
                    await Task.Delay(3000);
                    FrmIsVisiable = false;
                }
                else if (Email != "hung.ledinh@vti.com.vn" || Password != "1234560")
                {
                    FrmIsVisiable = true;
                    AlertMessage = ConstantMessage.WrongUserOrPass;
                    await Task.Delay(3000);
                    FrmIsVisiable = false;
                }
                else
                {
                    Busy = true;
                    BtnName = "";
                    await Task.Delay(2000);
                    await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                    Password = string.Empty;
                    Busy = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task HandleLoginSocial(string socialname)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
