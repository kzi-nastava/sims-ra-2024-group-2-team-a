using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBarPage : Page {
        private Frame _menuBarFrame, _mainFrame;
        private int _userId;
        public MenuBarPage(Frame menuBarF, Frame mainF, int userId) {
            InitializeComponent();
            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;
            _mainFrame.IsHitTestVisible = false;
            _mainFrame.Opacity = 0.6;
        }

        private void menuButton_Click(object sender, RoutedEventArgs e) {
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void homeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new ScheduledToursPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }
        private void removeButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Pay 5$ to Unlock", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        }

        private void followLiveButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new FollowLiveTourPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
            
        }

        private void statsButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Pay 5$ to Unlock", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

        }

        private void reviewsButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Pay 5$ to Unlock", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

        }

        private void requestsButton_Click(object sender, RoutedEventArgs e) {

            MessageBox.Show("Pay 5$ to Unlock", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        }
    }
}
