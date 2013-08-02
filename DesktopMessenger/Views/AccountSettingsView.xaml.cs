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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopMessenger.ViewModels;

namespace DesktopMessenger.Views
{
    /// <summary>
    /// Interaction logic for AccountSettingsView.xaml
    /// </summary>
    public partial class AccountSettingsView : UserControl
    {
        public AccountSettingsView()
        {
            InitializeComponent();
            DataContext = new AccountSettingsViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as AccountSettingsViewModel).Password = PasswordBox.SecurePassword;
        }
    }
}
