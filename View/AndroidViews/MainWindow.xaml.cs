using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Model;

namespace BookingApp.View.AndroidViews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Frame MainFrame { get; set; }

        public Frame SideFrame { get; set; }

        private readonly User _user;
        public MainWindow(User user)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            MainFrame.Content = new AccommodationPage(MainFrame,_user);
            SideFrame.Content = null;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
                sideFrame.Content = new SideMenuPage(MainFrame,SideFrame,_user,HeaderLabel);
                mainFrame.IsHitTestVisible = false;
                mainFrame.Opacity = 0.4;
        }   
    }
}
