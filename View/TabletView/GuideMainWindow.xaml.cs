using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.TabletView {
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

            _mainFrame.Content = new AddTourPage(mainFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void Logout(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

    }
}
