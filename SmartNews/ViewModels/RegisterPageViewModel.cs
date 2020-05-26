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
    public class RegisterPageViewModel
    {
        #region properties, variable
        public bool FrmIsVisiable { get; set; }
        public bool Busy { get; set; }
        public string AlertMessage { get; set; }
        public string Email { get; set; }
        public string RetypePassword { get; set; }
        public string BtnName { get; set; }
        public string Password { get; set; }
        #endregion
        #region command
        public ICommand SubmitCommand { get; set; }
        #endregion
        public RegisterPageViewModel()
        {
            BtnName = "Sign In";
            SubmitCommand = new Command((async) =>
            {
                HandleSubmit();
            });
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
                else
                {
                    Busy = true;
                    BtnName = "";
                    await Task.Delay(2000);
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    Password = string.Empty;
                    Busy = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
