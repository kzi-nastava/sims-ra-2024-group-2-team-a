using BookingApp.View.TabletView;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
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
        private void HamburgerBar_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void HamburgerBar_Executed(object sender, ExecutedRoutedEventArgs e) {
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void Home_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Home_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new ScheduledToursPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void AddTour_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AddTour_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void FollowLive_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void FollowLive_Executed(object sender, ExecutedRoutedEventArgs e) {
            MenuBarViewModel viewModel = new MenuBarViewModel(_userId);
            if (viewModel.IsTourActive())
                _mainFrame.Content = new LiveTourPage(viewModel.tourDTO, _mainFrame, _userId);
            else
                _mainFrame.Content = new FollowLiveTourPage(_userId);

            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void FinishedTours_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void FinishedTours_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new FinishedToursPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void Stats_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Stats_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new TourStatsPage(null, _mainFrame, _menuBarFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void Requests_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Requests_Executed(object sender, ExecutedRoutedEventArgs e) {
            MessageBox.Show("Pay 5$ to Unlock", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        }
    }
}
