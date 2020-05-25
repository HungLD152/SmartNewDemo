using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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
        public ICommand CheckedCommand { get; set; }
        public ICommand TextChangedCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand HiddenPasswordCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand NavigationRegisterCommand { get; set; }
        #endregion
    }
}
