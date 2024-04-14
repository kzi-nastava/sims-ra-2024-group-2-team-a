using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for FollowLiveTourPage.xaml
    /// </summary>
    public partial class FollowLiveTourPage : Page {
        public LiveTourViewModel ViewModel { get; set; }
        public FollowLiveTourPage(int userId) {
            InitializeComponent();
            ViewModel = new LiveTourViewModel(userId);
            DataContext = ViewModel;
        }

        private void filterButton_Click(object sender, RoutedEventArgs e) {
            ViewModel.FilterTours(SetFilter());
            itemsControlLiveTours.ItemsSource = ViewModel.tourDTOs;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e) {
            textBoxName.Text = string.Empty;
            textBoxTouristNumber.Text = string.Empty;
            textBoxDuration.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
        }
        

        private TourFilterDTO SetFilter() {
            string name = textBoxName.Text;
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            if (location == null) {
                location = new LocationDTO();
            }
            LanguageDTO language = (LanguageDTO)comboBoxLanguage.SelectedItem;
            if (language == null) {
                language = new LanguageDTO();
            }
            if (!int.TryParse(textBoxTouristNumber.Text, out int touristsNumber)) {
                textBoxTouristNumber.Text = string.Empty;
            }
            if (!int.TryParse(textBoxDuration.Text, out int duration)) {
                textBoxDuration.Text = string.Empty;
            }
            return  new TourFilterDTO(name, duration, touristsNumber, location, language, DateTime.MinValue);
        }
    }
}
