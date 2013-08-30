using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopMessenger.ViewModels;

namespace DesktopMessenger.Views
{
    /// <summary>
    /// Interaction logic for QuickbarView.xaml
    /// </summary>
    public partial class QuickbarView : Window
    {
        public QuickbarView()
        {
            InitializeComponent();
            DataContext = new QuickbarViewModel();
        }

<<<<<<< HEAD
        private void OnDrag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                
            }
=======
        private void expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
>>>>>>> e50ad9c1eaa9db630453d423d99489c0233002a8
        }
    }
}
