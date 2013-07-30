using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DesktopMessenger.Common;
using DesktopMessenger.ViewModels;

namespace DesktopMessenger.Commands
{
    internal class AddAcountCommand : ICommand
    {
        private readonly SettingsViewModel _viewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddAcountCommand(SettingsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanAddAccount;
        }

        public void Execute(object parameter)
        {
            _viewModel.AddAccount();
        }
    }
}
