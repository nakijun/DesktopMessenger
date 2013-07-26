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
        public ShowSettingsCommand(ContactListViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        private ContactListViewModel _viewModel;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ShowSettings();
        }


        public event EventHandler CanExecuteChanged;
    }
}
