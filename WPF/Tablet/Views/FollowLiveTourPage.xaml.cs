using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for FollowLiveTourPage.xaml
    /// </summary>
    public partial class FollowLiveTourPage : Page {
        public bool IsHelpEnabled { get; set; }
        public LiveTourViewModel ViewModel { get; set; }
        private int _userId;
        public FollowLiveTourPage(int userId) {
            _userId = userId;
            Window mainWindow = Application.Current.MainWindow;
            ProfileViewModel p = (ProfileViewModel)mainWindow.DataContext;
            ViewModel = new LiveTourViewModel(userId, p.guideProfileDTO);
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Clear_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e) {
            textBoxName.Text = string.Empty;
            textBoxTouristNumber.Text = string.Empty;
            textBoxDuration.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
        }

        private void Filter_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Filter_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.FilterTours(SetFilter());
            itemsControlLiveTours.ItemsSource = ViewModel.tourDTOs;
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

        private void Help_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if(ViewModel.guideProfileDTO.IsHelpActive) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e) {
            WizardWindow wizardWindow = new WizardWindow(_userId, 2, false);
            wizardWindow.ShowDialog();
        }
    }
}
