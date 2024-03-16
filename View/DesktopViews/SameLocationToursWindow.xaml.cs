﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace BookingApp.View.DesktopViews {
    /// <summary>
    /// Interaction logic for SameLocationToursWindow.xaml
    /// </summary>
    public partial class SameLocationToursWindow : Window {
        private TourRepository _tourRepository;
        private TourReservationWindow _parentWindow;
        public TourDTO CurrentTour { get; set; }
        public ObservableCollection<TourDTO> SameLocationTours { get; set; }
        public SameLocationToursWindow(TourDTO currentTour, TourReservationWindow parentWindow) {
            InitializeComponent();
            DataContext = this;
            CurrentTour = currentTour;
            _parentWindow = parentWindow;
            _tourRepository = new TourRepository();
            SameLocationTours = new ObservableCollection<TourDTO>();

            Update();
        }

        public void Update() {
            SameLocationTours.Clear();
            foreach (var tour in _tourRepository.GetSameLocationTours(CurrentTour))
                SameLocationTours.Add(new TourDTO(tour));
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e) {
            _parentWindow.Close();
            var button = (Button)sender;
            var selectedTour = (TourDTO)button.DataContext;

            if (_tourRepository.GetAvailableSpace(selectedTour) != 0) {
                TourReservationWindow reservationWindow = new TourReservationWindow(selectedTour, _parentWindow.UserId);
                reservationWindow.ShowDialog();
            }
        }
    }
}
