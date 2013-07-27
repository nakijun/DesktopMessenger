using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Models
{
    internal class Account : INotifyPropertyChanged
    {
        private string protocol;
        private string username;
        private string password;

        public Account(string protocol, string username, string password)
        {
            this.protocol = protocol;
            this.username = username;
            this.password = password;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                OnPropertyChanged("password");
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("username");
            }
        }

        public string Protocol
        {
            get { return protocol; }
            set
            {
                protocol = value;
                OnPropertyChanged("protocol");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Protocol.ToString();
        }
    }
}
