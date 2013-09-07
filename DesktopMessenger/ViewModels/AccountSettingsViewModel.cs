using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Common;
using DesktopMessenger.Common.Models;

namespace DesktopMessenger.ViewModels
{
    internal class AccountSettingsViewModel : ViewModelBase
    {
        private string _username;

        public ICommand AddAccountCommand { get; private set; }

        public string[] Services { get { return MessengerServiceFactory.Services; } }
        public string SelectedService { get; set; }
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }
        public SecureString Password { get; set; }

        public AccountSettingsViewModel()
        {
            AddAccountCommand = new DelegateCommand(AddAccount, CanAddAccount);
        }

        private bool CanAddAccount(object o)
        {
            return !String.IsNullOrWhiteSpace(SelectedService) && !String.IsNullOrWhiteSpace(Username) &&
                   (Password != null) && (Password.Length != 0);
        }

        private void AddAccount(object obj)
        {
            var service = MessengerServiceFactory.CreateInstance(SelectedService);
            var account = new Account {Username = Username, Password = Password, Service = service};
            AccountManager.Add(account);
            ServiceManager.Add(account);
        }
    }
}
