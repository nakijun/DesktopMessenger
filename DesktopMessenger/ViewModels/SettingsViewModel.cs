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
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<Account> _accounts = new ObservableCollection<Account>();
        private string _username;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddAccountCommand { get; private set; }
        public ObservableCollection<Account> Accounts { get { return AccountManager.Accounts; } }
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

        public SettingsViewModel()
        {
            AddAccountCommand = new AddAcountCommand(this);
        }

        public void AddAccount()
        {
            AccountManager.Add(new Account
                {
                    Username = Username,
                    Password = Password,
                    Service = MessengerServiceFactory.CreateInstance(SelectedService)
                });
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
