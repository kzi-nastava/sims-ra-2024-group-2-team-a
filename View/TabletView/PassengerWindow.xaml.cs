﻿using BookingApp.DTO;
using BookingApp.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for PassengerWindow.xaml
    /// </summary>
    public partial class PassengerWindow : Window {
        private int _tourId;
        private int _pointOfInterestId;
        private readonly PassengerRepository _passengerRepository;

        public PassengerDTO passengerDTO { get; set; }
        public ObservableCollection<PassengerDTO> passengerDTOs { get; set; }
        public List<PassengerDTO> joinedPassengerDTOs { get; set; }
        public PassengerWindow(int tourID, int pointOfInterestId) {
            InitializeComponent();
            DataContext = this;
            _tourId = tourID;
            _pointOfInterestId = pointOfInterestId;
            _passengerRepository = new PassengerRepository();
            Load();
        }

        private void joinPassengerTourComboBox_Click(object sender, RoutedEventArgs e) {
            var checkBox = (CheckBox)sender;
            var passenger = (PassengerDTO)checkBox.DataContext;
            if ((bool)checkBox.IsChecked)
                passenger.IsJoined = true;
            else
                passenger.IsJoined = false;

        }
        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
            this.Close();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e) {
            foreach (var passengerDTO in passengerDTOs) {
                if (passengerDTO.IsJoined)
                    passengerDTO.JoinedPointOfInterestId = _pointOfInterestId;
                _passengerRepository.Update(passengerDTO.ToModel());
            }
            this.DialogResult = true;
            this.Close();
        }

        private void Load() {
            passengerDTOs = new ObservableCollection<PassengerDTO>();
            joinedPassengerDTOs = new List<PassengerDTO>();
            foreach (var passenger in _passengerRepository.GetUnJoined(_tourId)) {
                passengerDTOs.Add(new PassengerDTO(passenger));
            }
        }
    }
}
