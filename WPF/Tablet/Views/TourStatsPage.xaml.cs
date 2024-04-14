using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;

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

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();
        }

        private void showButton_Click(object sender, RoutedEventArgs e) {
            int year = int.Parse( (string) yearComboBox.SelectedValue);
            Tour tour = ViewModel.GetMostViewedByYear(year);
            if (tour == null) {
                MessageBox.Show("Nema tura iz te godine", "NEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _mainFrame.Content = new TourStatsPage(new DTO.TourDTO(tour), _mainFrame, _menuBarFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }
    }
}
