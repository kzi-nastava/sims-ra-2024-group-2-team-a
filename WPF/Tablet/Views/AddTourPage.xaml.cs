using BookingApp.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for AddTour.xaml
    /// </summary>
    public partial class AddTourPage : Page {
        private Frame _mainFrame;
        private int _userId;

        public AddTourViewModel ViewModel { get; set; }

        public AddTourPage(Frame mainF, int userId) {
            InitializeComponent();
            _mainFrame = mainF;
            _userId = userId;
            ViewModel = new AddTourViewModel(userId);
            DataContext = ViewModel;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e) {
            ViewModel.AddTour();
            MessageBox.Show("Tour Added Succesfully", "Confirmed", MessageBoxButton.OK, MessageBoxImage.Information);
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
        }


        private void addPointOfInterestButton_Click(object sender, RoutedEventArgs e) {
            AddPointsOfInterestWindow pointOfInterestWindow = new AddPointsOfInterestWindow(ViewModel.pointOfInterestDTOs);
            pointOfInterestWindow.ShowDialog();
            confirmButton.IsEnabled = ViewModel.CheckValidation();
        }

        private void deletePointOfInterestButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var pointOfInterestDTO = (PointOfInterestDTO)button.DataContext;

            confirmButton.IsEnabled = ViewModel.DeletePointOfInterest(pointOfInterestDTO);
        }


        private void addDateTime_Click(object sender, RoutedEventArgs e) {
            AddBegginingDateTimeWindow begginingDateTimeWindow = new AddBegginingDateTimeWindow(ViewModel.begginings);
            begginingDateTimeWindow.ShowDialog();
        }

        private void deleteDateTime_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var beggining = (DateTime)button.DataContext;

            ViewModel.DeleteDateTime(beggining);
        }

        private void pickPhotosButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = SetOpenFile();
            List<string> absolutePaths = new List<string>();
            if (openFileDialog.ShowDialog() == true) {
                // Get absolute paths of selected images
                foreach (string filename in openFileDialog.FileNames) {
                    absolutePaths.Add(filename);
                }
                ViewModel.PickPhotos(absolutePaths);
            }
        }

        private OpenFileDialog SetOpenFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Milos\Desktop\SIMS\sims-ra-2024-group-2-team-a\Resources\Images\Tours\";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            return openFileDialog;
        }
    }
}