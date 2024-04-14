﻿using BookingApp.DTO;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristFollowLiveViewModel {
        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly PassengerService _passengerService = new PassengerService();
        public TourDTO SelectedTour { get; set; }
        public int UserId { get; set; }
        public ObservableCollection<PointOfInterestDTO> PointsOfInterest { get; set; }
        public ObservableCollection<PassengerDTO> Passengers { get; set; }
        public TouristFollowLiveViewModel(TourDTO selectedTour, int userId) {
            SelectedTour = selectedTour;
            UserId = userId;
            PointsOfInterest = new ObservableCollection<PointOfInterestDTO>();
            Passengers = new ObservableCollection<PassengerDTO>();
            Update();
        }

        public void Update() {
            PointsOfInterest.Clear();
            foreach (var point in _pointOfInterestService.GetAllByTourId(SelectedTour.Id)) {
                PointsOfInterest.Add(new PointOfInterestDTO(point));
            }

            Passengers.Clear();
            foreach (var passenger in _passengerService.GetPresent(UserId, SelectedTour)) {
                Passengers.Add(new PassengerDTO(passenger));
            }
        }
    }
}
