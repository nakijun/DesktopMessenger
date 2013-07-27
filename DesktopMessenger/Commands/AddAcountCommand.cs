using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DesktopMessenger.ViewModels;

namespace DesktopMessenger.Commands
{
    internal class AddAcountCommand : ICommand
    {
        private readonly SettingsViewModel _settingsViewModel;

        public AddAcountCommand(SettingsViewModel settingsViewModel)
        {
            // TODO: Complete member initialization
            _settingsViewModel = settingsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        

        public void Execute(object parameter)
        {
           _settingsViewModel.AddAccount("Facebook","demo","demo");
        }
    }
}
