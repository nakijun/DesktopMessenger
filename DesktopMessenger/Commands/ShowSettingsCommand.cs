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
        private ContactListViewModel _viewModel;

        public ShowSettingsCommand(ContactListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ShowSettings();
        }

        public event EventHandler CanExecuteChanged;
        #endregion
    }
}
