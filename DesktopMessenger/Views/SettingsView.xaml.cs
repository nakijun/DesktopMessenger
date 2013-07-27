using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using DesktopMessenger.Common;

namespace DesktopMessenger.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            //switch (accountTypeBox.Text)
            //{
            //    case "Facebook":
            //        TreeViewItem fbTreeViewItem = new TreeViewItem();
            //        fbTreeViewItem.Name = "facebookAccountTreeItem";
            //        fbTreeViewItem.Header = "Facebook";
            //        AccountsTreeViewItem.Items.Add(fbTreeViewItem);
            //}
        }
    }
}
