using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DesktopMessenger.ViewModels;

namespace DesktopMessenger.Commands
{
    internal class ShowContactListCommand : ICommand
    {
        private readonly QuickbarViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public ShowContactListCommand(QuickbarViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ShowContactList();
        }
    }
}
