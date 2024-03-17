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

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window {
        private int _userId;
        public Frame MenuBarFrame { get; set; }
        public Frame MainFrame { get; set; }
        public GuideMainWindow(int userId) {
            InitializeComponent();
            MainFrame = mainFrame;
            MenuBarFrame = menuBarFrame;
            _userId = userId;

            MainFrame.Content = new AddTourPage(mainFrame, _userId);
            MenuBarFrame.Content = new MenuBarButtonPage(MenuBarFrame, MainFrame, _userId);
        }

        private void Logout(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

    }
}
