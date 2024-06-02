using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page {
        private List<AccommodationDTO> _accommodationDTOs = new List<AccommodationDTO>();
        private List<LocationDTO> _locationDTOs = new List<LocationDTO>();

        private readonly AccommodationService _accommodationService = ServicesPool.GetService<AccommodationService>();
        private readonly AccommodationReservationService _accommodationReservationService = ServicesPool.GetService<AccommodationReservationService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly OwnerService _ownerService = ServicesPool.GetService<OwnerService>();

        public bool QuickBookEnabled => toggleSwitch.IsChecked;

        public BookingPage() {
            InitializeComponent();
            Update();
            SetItemSources();
        }

        public void Update() {
            UpdateLocationDTOs();
            UpdateAccommodationDTOs(_accommodationService.GetAll());
        }

        private void UpdateLocationDTOs() {
            var locations = _locationService.GetAll();
            _locationDTOs = locations.Select(l => new LocationDTO(l)).ToList();
            _locationDTOs.Insert(0, new LocationDTO());
        }

        public void UpdateAccommodationDTOs(List<Accommodation> accommodations) {
            _accommodationDTOs = accommodations.Select(a => new AccommodationDTO(a)).ToList();

            foreach (var acc in _accommodationDTOs) {
                var loc = _locationDTOs.Find(l => l.Id == acc.LocationId);
                acc.Location = loc;
            }

            _accommodationDTOs = _accommodationDTOs.OrderByDescending(a => IsAccommodationOwnerSuper(a.OwnerId)).ToList();
            itemsControlAccommodations.ItemsSource = _accommodationDTOs;
        }

        private bool IsAccommodationOwnerSuper(int ownerId) {
            return _ownerService.GetByUserId(ownerId).IsSuper;
        }

        private void SetItemSources() {
            itemsControlAccommodations.ItemsSource = _accommodationDTOs;

            comboBoxLocation.ItemsSource = _locationDTOs;
            comboBoxLocation.SelectedIndex = 0;

            comboBoxType.ItemsSource = Enum.GetValues(typeof(AccommodationType));
            comboBoxType.SelectedIndex = 0;

            quickDatePickerStartDate.DisplayDateStart = DateTime.Today.AddDays(1);
            quickDatePickerEndDate.IsEnabled = false;
        }

        private void UpdateDatePicker(object sender, SelectionChangedEventArgs e) {
            if (quickDatePickerStartDate.SelectedDate != null) {
                quickDatePickerEndDate.IsEnabled = true;
                quickDatePickerEndDate.DisplayDateStart = quickDatePickerStartDate.SelectedDate.Value.AddDays(1);
                return;
            }
        }

        private void ButtonFilterClick(object sender, RoutedEventArgs e) {
            if (toggleSwitch.IsChecked) {
                ApplyQuickFilter();
            } else {
                ApplyRegularFilter();
            }
        }

        private void ApplyRegularFilter() {
            string name = textBoxName.Text;
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            AccommodationType type = (AccommodationType)comboBoxType.SelectedItem;

            if (!int.TryParse(textBoxGuests.Text, out int guestNumber)) {
                textBoxGuests.Text = "";
            }

            if (!int.TryParse(textBoxDays.Text, out int reservationDays)) {
                textBoxDays.Text = "";
            }

            AccommodationFilterDTO filter = new AccommodationFilterDTO(name, location, type, guestNumber, reservationDays);

            var accommodations = _accommodationService.GetFilteredAccommodations(filter);
            UpdateAccommodationDTOs(accommodations);
        }

        private void ApplyQuickFilter() {
            DateOnly startDate = new DateOnly();
            DateOnly endDate = new DateOnly();

            if (!int.TryParse(quickTextBoxGuests.Text, out int guestNumber)) {
                textBoxGuests.Text = "";
            }

            if (!int.TryParse(quickTextBoxDays.Text, out int reservationDays)) {
                textBoxDays.Text = "";
            }

            if(quickDatePickerStartDate.SelectedDate != null) {
                startDate = DateOnly.FromDateTime(quickDatePickerStartDate.SelectedDate.Value);
            }

            if (quickDatePickerEndDate.SelectedDate != null) {
                endDate = DateOnly.FromDateTime(quickDatePickerEndDate.SelectedDate.Value);
            }


            AccommodationFilterDTO accFilter = new AccommodationFilterDTO("", new LocationDTO(), AccommodationType.any, guestNumber, reservationDays);
            AccommodationReservationFilterDTO resFilter = new AccommodationReservationFilterDTO(0, reservationDays, guestNumber, startDate, endDate);

            var accommodations = _accommodationService.GetAvailableAccommodationsInDateRange(accFilter, resFilter);
            UpdateAccommodationDTOs(accommodations);
        }

        private void ButtonClearClick(object sender, RoutedEventArgs e) {
            comboBoxLocation.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
            textBoxDays.Text = "";
            textBoxGuests.Text = "";
            textBoxName.Text = "";

            quickDatePickerStartDate.SelectedDate = null;
            quickDatePickerEndDate.SelectedDate = null;
            quickDatePickerEndDate.IsEnabled = false;
            quickTextBoxDays.Text = "";
            quickTextBoxGuests.Text = "";

            ButtonFilterClick(null, null);
        }

        public void OpenQuickBookDialog(AccommodationDTO accommodation) {
            rectBlurBackground.Visibility = Visibility.Visible;

            int.TryParse(quickTextBoxGuests.Text, out int guestNumber);
            int.TryParse(quickTextBoxDays.Text, out int reservationDays);

            DateOnly startDate = new DateOnly();
            DateOnly endDate = new DateOnly();

            if (quickDatePickerStartDate.SelectedDate != null) {
                startDate = DateOnly.FromDateTime(quickDatePickerStartDate.SelectedDate.Value);
            }

            if (quickDatePickerEndDate.SelectedDate != null) {
                endDate = DateOnly.FromDateTime(quickDatePickerEndDate.SelectedDate.Value);
            }

            AccommodationReservationFilterDTO resFilter = new AccommodationReservationFilterDTO(
                accommodation.Id,
                reservationDays, 
                guestNumber,
                startDate,
                endDate);

            mainGrid.Children.Add(new QuickBookModalDialog(this, accommodation, resFilter));
        }

        public void CloseModalDialog() {
            rectBlurBackground.Visibility = Visibility.Hidden;
            mainGrid.Children.RemoveAt(mainGrid.Children.Count - 1);
        }
    }
}
