﻿using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourReviewsPage.xaml
    /// </summary>
    public partial class TourReviewsPage : Page {
        private Frame _mainFrame;
        public TourReviewViewModel ViewModel { get; set; }

        public TourReviewsPage(TourDTO tDTO, Frame mFrame) {
            InitializeComponent();
            ViewModel = new TourReviewViewModel(tDTO);
            DataContext = ViewModel;

            _mainFrame = mFrame;
        }

        private void Close_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            _mainFrame.GoBack();
        }
    }
}
