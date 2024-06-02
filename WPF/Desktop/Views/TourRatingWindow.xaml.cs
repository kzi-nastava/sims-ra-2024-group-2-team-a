using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TourRatingWindow.xaml
    /// </summary>
    public partial class TourRatingWindow : Window
    {
        TourRatingWindowViewModel viewModel;
        public TourRatingWindow(TourDTO selectedTour, int userId)
        {
            InitializeComponent();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.7;
            this.Height = screenHeight * 0.7;

            viewModel = new TourRatingWindowViewModel(selectedTour, userId);
            DataContext = viewModel;
        }

        private void ConfirmationButton_Click(object sender, RoutedEventArgs e) {
            viewModel.SendReview();
            this.Close();
        }
    }
}
