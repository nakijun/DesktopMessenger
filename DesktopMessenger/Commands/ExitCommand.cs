using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DesktopMessenger.Commands
{
    internal class ExitCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        private ViewModels.ContactListViewModel _contactListViewModel;

        public ExitCommand(ViewModels.ContactListViewModel contactListViewModel)
        {
            // TODO: Complete member initialization
            _contactListViewModel = contactListViewModel;
        }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
