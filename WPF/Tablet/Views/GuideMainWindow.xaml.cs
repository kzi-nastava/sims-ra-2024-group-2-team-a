using BookingApp.View;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window {
        private int _userId;
        private Frame _menuBarFrame { get; set; }
        private Frame _mainFrame { get; set; }
        public GuideMainWindow(int userId) {
            InitializeComponent();
            _mainFrame = mainFrame;
            _menuBarFrame = menuBarFrame;
            _userId = userId;

            _mainFrame.Content = new ScheduledToursPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void Logout(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

    }
}
