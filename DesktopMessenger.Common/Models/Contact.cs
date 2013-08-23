using System.ComponentModel;
using System.Drawing;

namespace DesktopMessenger.Common.Models
{
    public class Contact : INotifyPropertyChanged
    {
        private Bitmap _picture;
        private string _name;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Id { get; private set; }
        public Bitmap Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                OnPropertyChanged("Picture");
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public Contact(string id)
        {
            Id = id;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}