using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Common;
using DesktopMessenger.Models;
using DesktopMessenger.Views;

namespace DesktopMessenger.ViewModels
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly AccountSettingsViewModel _accountSettingsViewModel;
        private Page _currentPage;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Account> Accounts
        {
            get { return AccountManager.Accounts; }
        }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public SettingsViewModel()
        {
            _accountSettingsViewModel = new AccountSettingsViewModel();

            //TODO show general settings
            CurrentPage = new AccountSettingsView {DataContext = _accountSettingsViewModel};
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
