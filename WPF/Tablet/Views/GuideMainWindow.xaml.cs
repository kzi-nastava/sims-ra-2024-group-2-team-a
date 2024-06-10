using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window {
        private int _userId;
        private Frame _menuBarFrame { get; set; }
        private Frame _mainFrame { get; set; }
        ProfileViewModel ViewModel { get; set; }
        public GuideMainWindow(int userId) {
            Application.Current.MainWindow = this;
            InitializeComponent();

            ViewModel = new ProfileViewModel(userId);
            ViewModel.Update();

            DataContext = ViewModel;

            _mainFrame = mainFrame;
            _menuBarFrame = menuBarFrame;
            _userId = userId;

            _mainFrame.Content = new ScheduledToursPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, additionalFrame, _userId);

        }

        private void Profile_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Profile_Executed(object sender, ExecutedRoutedEventArgs e) {
            ProfilePage profilePage = new ProfilePage(_userId, this, _mainFrame);
            additionalFrame.Content = null;
            _mainFrame.Content = profilePage; 
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, additionalFrame, _userId);
        }

        private void Help_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;

        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (ViewModel.guideProfileDTO.IsHelpActive) {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to DISABLE help?", "Disable Help", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.No)
                    return;

            }
            else {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to ENABLE help?", "Enable Help", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.No)
                    return;
            }
            ViewModel.HelpPressed();
        }
    }
}
