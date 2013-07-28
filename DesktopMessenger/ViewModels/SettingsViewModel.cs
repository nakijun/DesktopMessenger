using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<Account> Accounts { get { return _accounts; } }
        public string[] Adapters { get { return MessengerAdapterFactory.Adapters; } }
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

        public bool CanAddAccount
        {
            get { return !(String.IsNullOrWhiteSpace(SelectedService) || String.IsNullOrWhiteSpace(Username) || String.IsNullOrWhiteSpace("Password")); } //TODO read password from passwordbox _securely_
        }

        public SettingsViewModel()
        {
            AddAccountCommand = new AddAcountCommand(this);
        }

        public void AddAccount(IMessengerAdapter adapter, string username, string password)
        {
            _accounts.Add(new Account(adapter, username, password));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
