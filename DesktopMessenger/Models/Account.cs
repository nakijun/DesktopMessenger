using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using DesktopMessenger.Common;

namespace DesktopMessenger.Models
{
    internal class Account
    {
        private readonly string _password; //TODO use securestring

        public Account(IMessengerService service, string username, string password)
        {
            Service = service;
            Username = username;
            _password = password;
        }

        public IMessengerService Service { get; set; }
        public string Username { get; set; }
    }
}
