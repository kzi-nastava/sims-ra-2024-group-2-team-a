using BookingApp.Domain.Model;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourStatsPage.xaml
    /// </summary>
    public partial class TourStatsPage : Page {
        private Frame _mainFrame, _menuBarFrame;
        private int _userId;

        public TourStatsViewModel ViewModel { get; set; }
        public TourStatsPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            InitializeComponent();
            ViewModel = new TourStatsViewModel(tDTO, userId);
            DataContext = ViewModel;

            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;

        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.GoBack();

        }
        private void Show_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }


        private void Show_Executed(object sender, ExecutedRoutedEventArgs e) {
            int year = int.Parse((string)yearComboBox.SelectedValue);

            if (!ViewModel.GetMostViewedByYear(year)) {
                MessageBox.Show("Nema tura iz te godine", "NEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _mainFrame.Content = new TourStatsPage(ViewModel.tourDTO, _mainFrame, _menuBarFrame, _userId);
            
        }
    }
}
