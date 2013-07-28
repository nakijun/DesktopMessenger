using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Views;
using DesktopMessenger.Models;
using DesktopMessenger.Properties;

namespace DesktopMessenger.ViewModels
{
    internal class ContactListViewModel
    {
        private readonly Contact _contact;

        public ContactListViewModel()
        {
            ShowSettingsCommand = new ShowSettingsCommand(this);
            ExitCommand = new ExitCommand(this);
            _contact = new Contact(Guid.NewGuid().ToString(), Resources.status_online.ToBitmap(), "Contact1"); //TODO use real data in the future
        }

        public ICommand ShowSettingsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public Contact Contact
        {
            get { return _contact; }
        }


        public void ShowSettings()
        {
            SettingsView settings = new SettingsView();
            settings.ShowDialog();
        }
    }
}
