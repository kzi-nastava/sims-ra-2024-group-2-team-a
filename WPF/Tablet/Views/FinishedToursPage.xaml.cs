using BookingApp.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views{
    /// <summary>
    /// Interaction logic for FinishedToursPage.xaml
    /// </summary>
    public partial class FinishedToursPage : Page {
        public FinishedTourViewModel ViewModel { get; set; }

        public FinishedToursPage(int userId){
            InitializeComponent();
            ViewModel = new FinishedTourViewModel(userId);
            DataContext = ViewModel;
        }

        private void filterButton_Click(object sender, RoutedEventArgs e) {
            ViewModel.FilterTours(SetFilter());
            itemsControlScheduledTours.ItemsSource = ViewModel.tourDTOs;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e) {
            textBoxName.Text = string.Empty;
            textBoxTouristNumber.Text = string.Empty;
            textBoxDuration.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
            datePicker.SelectedDate = DateTime.MinValue;
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
            DateOnly date;
            if (!datePicker.SelectedDate.HasValue)
                date = DateOnly.MinValue;
            else
                date = DateOnly.FromDateTime(datePicker.SelectedDate.Value);

            DateTime beggining = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            return new TourFilterDTO(name, duration, touristsNumber, location, language, beggining);
        }
    }
}
