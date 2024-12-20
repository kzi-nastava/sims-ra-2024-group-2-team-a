﻿using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for PassengerWindow.xaml
    /// </summary>
    public partial class PassengerWindow : Window {
        public PassengerWindowViewModel ViewModel { get; set; }
        public PassengerWindow(int tourID, int pointOfInterestId) {
            InitializeComponent();
            ViewModel = new PassengerWindowViewModel(tourID, pointOfInterestId);
            DataContext = ViewModel;
        }

        private void joinPassengerTourComboBox_Click(object sender, RoutedEventArgs e) {
            var checkBox = (CheckBox)sender;
            var passenger = (PassengerDTO)checkBox.DataContext;
            if ((bool)checkBox.IsChecked)
                passenger.IsJoined = true;
            else
                passenger.IsJoined = false;

        }
        private void Cancel_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            this.DialogResult = false;
            this.Close();
        }

        private void Confirm_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Confirm_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            ViewModel.JoinPassengers();
            this.DialogResult = true;
            this.Close();
        }
    }
}
