using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Tablet.Views
{
    /// <summary>
    /// Interaction logic for TourRequestsPage.xaml
    /// </summary>
    public partial class TourRequestsPage : Page {
        private Frame _additionalFrame;
        public TourRequestViewModel ViewModel { get; set; }
        public TourRequestsPage(Frame aFrame, int userId)
        {
            InitializeComponent();
            _additionalFrame = aFrame;
            ViewModel = new TourRequestViewModel(userId, false);
            DataContext = ViewModel;
        }
        private void Clear_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e) {
            ClearFilters();
        }

        private void Filter_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Filter_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.FilterRequests(SetFilter());
            itemsControlTourRequests.ItemsSource = ViewModel.tourRequestDTOs;
        }
        private TourRequestFilterDTO SetFilter() {
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            if (location == null)
                location = new LocationDTO();

            LanguageDTO language = (LanguageDTO)comboBoxLanguage.SelectedItem;
            if (language == null)
                language = new LanguageDTO();

            if (!int.TryParse(textBoxTourists.Text, out int touristsNumber))
                textBoxTourists.Text = string.Empty;

            DateOnly start;
            if (datePickerFrom.SelectedDate.HasValue)
                start = DateOnly.FromDateTime(datePickerFrom.SelectedDate.Value);
            else
                start = DateOnly.MinValue;

            DateOnly end;
            if (datePickerTo.SelectedDate.HasValue)
                end = DateOnly.FromDateTime(datePickerTo.SelectedDate.Value);
            else
                end = DateOnly.MaxValue;

            return new TourRequestFilterDTO(touristsNumber, location, language, start, end);
        }
        private void ClearFilters() {
            textBoxTourists.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
            datePickerFrom.SelectedDate = null;
            datePickerTo.SelectedDate = null;
        }
    }
}
