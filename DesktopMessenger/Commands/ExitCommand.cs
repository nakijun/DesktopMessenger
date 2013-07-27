﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DesktopMessenger.ViewModels;

namespace DesktopMessenger.Commands
{
    internal class ExitCommand : ICommand
    {
        private ContactListViewModel _contactListViewModel;

        public ExitCommand(ContactListViewModel contactListViewModel)
        {
            _contactListViewModel = contactListViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
