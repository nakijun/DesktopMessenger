
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Models
{
    public class Contact : INotifyPropertyChanged
    {
        private Bitmap _picture;
        private string _name;

        public event PropertyChangedEventHandler PropertyChanged;

        public Contact(string id, Bitmap pic, string name)
        {
            Id = id;
            _picture = pic;
            _name = name;
        }

        public string Id { get; private set; }

        public Bitmap Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                OnPropertyChanged("pic");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}