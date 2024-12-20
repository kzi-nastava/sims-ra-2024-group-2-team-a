﻿using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourPage.xaml
    /// </summary>
    public partial class TourPage : Page {

        private Frame _mainFrame, _menuBarFrame;
        private int _userId;
        
        public ScheduledTourViewModel ViewModel {  get; set; }
        public TourPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            InitializeComponent();
            ViewModel = new ScheduledTourViewModel(tDTO, userId);
            DataContext = ViewModel;

            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
        }
        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.GoBack();
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (ViewModel.tourDTO.Beggining < DateTime.Now.AddHours(48)) {
                MessageBox.Show("Tour is scheduled in the next 48 hours. Too late to delete it!", "Unable to delete tour", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this tour?", "DELETE this tour", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            ViewModel.DeleteReservations();
            ViewModel.DeleteTour();

            _mainFrame.Content = new ScheduledToursPage(_userId);
        }
    }
}
