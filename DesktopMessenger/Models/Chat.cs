using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Models
{
    internal class Chat : INotifyPropertyChanged
    {
        private string _title;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    OnPropertyChanged("Title");
                    _title = value;
                }
            }
        }

        public ObservableCollection<Message> Messages { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
