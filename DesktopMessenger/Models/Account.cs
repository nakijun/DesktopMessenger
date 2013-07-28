using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DesktopMessenger.Common;

namespace DesktopMessenger.Models
{
    internal class Account
    {
        private readonly string _password; //TODO use securestring

        public Account(IMessengerAdapter adapter, string username, string password)
        {
            Adapter = adapter;
            Username = username;
            _password = password;
        }

        public IMessengerAdapter Adapter { get; set; }
        public string Username { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Username, Adapter.ServiceName);
        }
    }
}
