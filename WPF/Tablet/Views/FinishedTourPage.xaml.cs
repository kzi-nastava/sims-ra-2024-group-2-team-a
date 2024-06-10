using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for FinishedTourPage.xaml
    /// </summary>
    public partial class FinishedTourPage : Page {
        private Frame _mainFrame, _menuBarFrame;
        private int _userId;
        public FinishedTourViewModel ViewModel { get; set; }
        public FinishedTourPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            Window mainWindow = Application.Current.MainWindow;
            ProfileViewModel p = (ProfileViewModel)mainWindow.DataContext;

            ViewModel = new FinishedTourViewModel(tDTO, userId, p.guideProfileDTO);
            DataContext = ViewModel;
            InitializeComponent();

            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.GoBack();
        }

        private void Stats_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Stats_Executed(object sender,  ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new TourStatsPage(ViewModel.tourDTO, _mainFrame, _menuBarFrame, _userId);
        }

        private void Reviews_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
        private void Reviews_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new TourReviewsPage(ViewModel.tourDTO, _mainFrame);
        }

        private void Help_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (ViewModel.guideProfileDTO.IsHelpActive) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e) {
            WizardWindow wizardWindow = new WizardWindow(_userId, 3, false);
            wizardWindow.ShowDialog();
        }
    }
}
