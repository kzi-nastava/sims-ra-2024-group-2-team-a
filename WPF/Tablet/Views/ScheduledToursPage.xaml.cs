using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for ScheduledToursPage.xaml
    /// </summary>
    public partial class ScheduledToursPage : Page {

        public ScheduledTourViewModel ViewModel { get; set; }
        public ScheduledToursPage(int userId) {
            InitializeComponent();
            ViewModel = new ScheduledTourViewModel(userId);
            DataContext = ViewModel;
        }

        private void Clear_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e) {
            textBoxName.Text = string.Empty;
            textBoxTouristNumber.Text = string.Empty;
            textBoxDuration.Text = string.Empty;
            textBoxTime.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
            datePicker.SelectedDate = null;
        }
        private void Filter_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
        private void Filter_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.FilterTours(SetFilter());
            itemsControlScheduledTours.ItemsSource = ViewModel.tourDTOs;
        }
        private TourFilterDTO SetFilter() {
            string name = textBoxName.Text;
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            if (location == null) 
                location = new LocationDTO();
            
            LanguageDTO language = (LanguageDTO)comboBoxLanguage.SelectedItem;
            if (language == null) 
                language = new LanguageDTO();

            if (!int.TryParse(textBoxTouristNumber.Text, out int touristsNumber)) 
                textBoxTouristNumber.Text = string.Empty;
            
            if (!int.TryParse(textBoxDuration.Text, out int duration)) 
                textBoxDuration.Text = string.Empty;

            DateOnly date;
            if (datePicker.SelectedDate.HasValue)
                date = DateOnly.FromDateTime(datePicker.SelectedDate.Value);
            else
                date = DateOnly.MinValue;
            int time;
            int.TryParse(textBoxTime.Text, out time);
            DateTime beggining = new DateTime(date.Year, date.Month, date.Day, time, 0, 0);

            return new TourFilterDTO(name, duration, touristsNumber, location, language, beggining);
        }

    }
}
