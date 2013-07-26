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

        public Contact(Bitmap pic, string name)
        {
            _picture = pic;
            _name = name;
        }

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


        public event PropertyChangedEventHandler PropertyChanged;
        private string p1;
        private string p2;

        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(p));
            }
        }
    }
}
