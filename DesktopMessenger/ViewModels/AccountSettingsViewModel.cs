using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Common;
using DesktopMessenger.Models;

namespace DesktopMessenger.ViewModels
{
    internal class AccountSettingsViewModel : INotifyPropertyChanged
    {
        private string _username;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddAccountCommand { get; private set; }
        public string[] Services { get { return MessengerServiceFactory.Services; } }
        public string SelectedService { get; set; }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public SecureString Password { get; set; }

        public bool CanAddAccount
        {
            get
            {
                return
                    !String.IsNullOrWhiteSpace(SelectedService) && !String.IsNullOrWhiteSpace(Username) &&
                    (Password != null) && (Password.Length != 0);
            }
        }

        public AccountSettingsViewModel()
        {
            AddAccountCommand = new AddAcountCommand(this);
        }

        public void AddAccount()
        {
            var service = MessengerServiceFactory.CreateInstance(SelectedService);
            var account = new Account {Username = Username, Password = Password, Service = service};
            AccountManager.Add(account);
            //ServiceManager.Connect(service, account);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
