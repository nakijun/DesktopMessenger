using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DesktopMessenger.Commands;
using DesktopMessenger.Common;
using System.Windows.Input;
using DesktopMessenger.Models;
using System.ComponentModel;

namespace DesktopMessenger.ViewModels
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Account> accounts; 

        public SettingsViewModel()
        {
            AddAccountCommand = new AddAcountCommand(this);
            accounts = new ObservableCollection<Account>();
            accounts.Add(new Account("Facebook","demo","demo"));
        }

        public Account Account { get; private set; }

        public ICommand AddAccountCommand { get; private set; }
        public ObservableCollection<Account> Accounts
        {
            get { return accounts; }
        }

        public void AddAccount(string protocol, string username, string password)
        {
            accounts.Add(new Account(protocol,username,password));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
