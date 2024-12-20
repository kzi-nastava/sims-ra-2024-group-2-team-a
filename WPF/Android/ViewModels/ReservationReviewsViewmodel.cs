﻿using System;
using BookingApp.Services;
using System.Collections.ObjectModel;
using BookingApp.WPF.Android.Views;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingApp.WPF.DTO;
using BookingApp.Domain.Model;
using BookingApp.WPF.Utils.Commands;

namespace BookingApp.WPF.Android.ViewModels {
    public class ReservationReviewsViewmodel : INotifyPropertyChanged {

        private AccommodationService accommodationService = ServicesPool.GetService<AccommodationService>(); 

        private AccommodationReviewService reviewService = ServicesPool.GetService<AccommodationReviewService>();

        private AccommodationReservationService accReservationService = ServicesPool.GetService<AccommodationReservationService>();

        private AccommodationRescheduleRequestService requestService = ServicesPool.GetService<AccommodationRescheduleRequestService>();

        private User _user;
        public ObservableCollection<AccommodationReservationDTO> ReservationCollection { get; set; }
        public ObservableCollection<AccommodationRescheduleRequestDTO> RescheduleRequestDTOs { get; set; }

        public AccommodationReservationDTO _selectedReservation;
        public AccommodationReservationDTO SelectedReservation {
            get {
                return _selectedReservation;
            }
            set {
                if (value != _selectedReservation) {
                    _selectedReservation = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccommodationRescheduleRequestDTO SelectedRequest { get; set; }
        public AndroidCommand AcceptButtonCommand{ get; set;}
        public AndroidCommand DeclineButtonCommand { get; set; }
        public ReservationReviewsViewmodel(User user) {
            _user = user;

            ReservationCollection = new ObservableCollection<AccommodationReservationDTO>();
            RescheduleRequestDTOs = new ObservableCollection<AccommodationRescheduleRequestDTO>();

            Update();

            AcceptButtonCommand = new AndroidCommand(AcceptButton_Executed, AcceptButton_CanExecute);
            DeclineButtonCommand = new AndroidCommand(DeclineButton_Executed , DeclineButton_CanExecute);
        }

        public void Update() {
            FillReservationCollection();
            FillRescheduleRequestCollection();
        }

        private void FillRescheduleRequestCollection() {
            RescheduleRequestDTOs.Clear();
            foreach (AccommodationRescheduleRequest request in requestService.GetSortedRequestsByOwnerId(_user.Id)) {
                AccommodationRescheduleRequestDTO rescheduleRequestDTO = new AccommodationRescheduleRequestDTO(request);
                AccommodationReservation accommodationReservation = accReservationService.GetById(request.ReservationId);

                rescheduleRequestDTO.SetDates();
                rescheduleRequestDTO.AccommodationName = accommodationService.GetById(accommodationReservation.AccommodationId).Name;
                rescheduleRequestDTO.IsAvailable =
                    accReservationService.CheckAccommodationAvailability(accommodationReservation.AccommodationId, rescheduleRequestDTO.NewStartDate, rescheduleRequestDTO.NewEndDate);
                RescheduleRequestDTOs.Add(rescheduleRequestDTO);
            }
        }
        private void FillReservationCollection() {
            ReservationCollection.Clear();

            foreach (var acc in accommodationService.GetByOwnerId(_user.Id)) {
                foreach (var accRes in accReservationService.GetByAccommodationId(acc.Id)) {
                    AccommodationReservationDTO accResDTO = new AccommodationReservationDTO(accRes);

                    AccommodationService accService = ServicesPool.GetService<AccommodationService>();
                    accResDTO.Accommodation = new AccommodationDTO(accService.GetById(accResDTO.AccommodationId));

                    if (reviewService.IsGradedByGuest(accResDTO.Id)) {
                        accResDTO.IsGradedByGuest = true;
                    }
                    if (reviewService.IsGradedByOwner(accResDTO.Id)) {
                        accResDTO.IsGradedByOwner = true;
                    }

                    ReservationCollection.Add(accResDTO);
                }
            }
        }
        public bool AssignGradeButton() {
            if (SelectedReservation == null) {
                return false;
            }
            if(SelectedReservation.CanBeGradedByOwner){
                AssignGradeWindow assignGradeWindow = new AssignGradeWindow(SelectedReservation, _user.Id, this);
                assignGradeWindow.ShowDialog();
            }
            return true;
        }

        public string ViewGradeButton() {
            if (SelectedReservation == null) {
                return "Please select a reservation first!";
            }
            else if (!SelectedReservation.IsGradedByOwner) { 
                return "You must grade this reservation first!";
            }
            else {
                ViewGuestGradeWindow viewGuestGradeWindow = new ViewGuestGradeWindow(SelectedReservation);
                viewGuestGradeWindow.ShowDialog();
                return null;
            }
        }

        public bool AcceptButton_CanExecute(object obj) {
            if (SelectedRequest == null)
                return true;
            else if (SelectedRequest.Status != RescheduleRequestStatus.Pending) {
                return false;
            }
            else {
                return true;
            }
        }
        public void AcceptButton_Executed(object obj) {
            if (SelectedRequest == null)
                return;

            AccommodationStatisticsService statisticsService = ServicesPool.GetService<AccommodationStatisticsService>();
            AccommodationReservation accommodationReservation = accReservationService.GetById(SelectedRequest.ReservationId);

            statisticsService.UpdatePostponedStatistics(accommodationReservation.AccommodationId, SelectedRequest.OldStartDate, 
                SelectedRequest.OldStartDate.AddDays(SelectedRequest.Duration) , SelectedRequest.NewStartDate, SelectedRequest.NewEndDate, accommodationReservation.GuestsNumber);

            accommodationReservation.StartDate = SelectedRequest.NewStartDate;
            accommodationReservation.EndDate = SelectedRequest.NewEndDate;
            accReservationService.Update(accommodationReservation);

            AccommodationRescheduleRequest rescheduleRequest = SelectedRequest.ToRescheduleRequest();
            rescheduleRequest.Status = RescheduleRequestStatus.Approved;
            requestService.Update(rescheduleRequest);

            
            this.Update();
        }

        public bool DeclineButton_CanExecute(object obj) {
            if (SelectedRequest == null)
                return true;
            else if (SelectedRequest.Status != RescheduleRequestStatus.Pending) {
                return false;
            }
            else {
                return true;
            }
        }

        public void DeclineButton_Executed(object obj) {
            if (SelectedRequest == null)
                return;

            RescheduleRequestDeclineWindow rescheduleRequestDeclineWindow = new RescheduleRequestDeclineWindow(SelectedRequest);
            rescheduleRequestDeclineWindow.ShowDialog();
            this.Update();
        }

        public void RequestsListSelectionChanged() {
            AcceptButtonCommand.RaiseCanExecuteChanged();
            DeclineButtonCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

