﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace BookingApp.View.WebViews
{
    /// <summary>
    /// Interaction logic for RescheduleReservationModalDialog.xaml
    /// </summary>
    public partial class RescheduleReservationModalDialog : UserControl
    {
        private readonly RescheduleRequestRepository _rescheduleRepository = new RescheduleRequestRepository();

        private AccommodationReservationDTO _reservation;
        private ReservationsPage _parentPage;

        public RescheduleReservationModalDialog(ReservationsPage parentPage, AccommodationReservationDTO reservation)
        {
            InitializeComponent();
            _reservation = reservation;
            _parentPage = parentPage;
            DataContext = _reservation;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            var accommodation = accommodationRepository.GetById(_reservation.AccommodationId);
            DateOnly newDate = DateOnly.FromDateTime(dataPickerNewDate.SelectedDate.Value);

            RescheduleRequest newRequest = new RescheduleRequest(
                RescheduleRequestStatus.Pending,
                _reservation.Id,
                _reservation.GuestId,
                accommodation.OwnerId,
                _reservation.StartDate,
                newDate,
                "");

            _rescheduleRepository.Save(newRequest);
            _parentPage.CloseModalDialog();
        }
    }
}