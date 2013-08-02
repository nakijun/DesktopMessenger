using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DesktopMessenger.ViewModels;

namespace DesktopMessenger.Commands
{
    internal class ShowSettingsCommand : ICommand
    {
        private readonly ContactListViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public ShowSettingsCommand(ContactListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ShowSettings();
        }
    }
}
