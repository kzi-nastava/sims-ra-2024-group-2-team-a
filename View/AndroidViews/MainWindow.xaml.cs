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

        private readonly User _user;
        public MainWindow(User user)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            _user = user;
            MainFrame.Content = new AccomodationPage(MainFrame,_user);
        }
    }
}
