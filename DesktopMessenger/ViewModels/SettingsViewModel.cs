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
    internal class SettingsViewModel
    {
        public ObservableCollection<Account> Accounts { get { return AccountManager.Accounts; } }
    }
}
