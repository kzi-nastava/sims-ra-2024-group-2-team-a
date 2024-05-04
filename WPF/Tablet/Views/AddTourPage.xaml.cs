using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for AddTour.xaml
    /// </summary>
    public partial class AddTourPage : Page {
        private Frame _mainFrame;
        private int _userId;

        public AddTourViewModel ViewModel { get; set; }

        public AddTourPage(Frame mainF, int userId) {
            ViewModel = new AddTourViewModel(userId);
            InitializeComponent();
            _mainFrame = mainF;
            _userId = userId;
            DataContext = ViewModel;
        }

        private void Reset_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Reset_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
        }
        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (ViewModel.CheckValidation()) //ubaci uslov da su sva polja popunjena
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.AddTour();
            MessageBox.Show("Tour Added Succesfully", "Confirmed", MessageBoxButton.OK, MessageBoxImage.Information);
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
        }

        private void AddDateTime_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AddDateTime_Executed(object sender, ExecutedRoutedEventArgs e) {
            AddBegginingDateTimeWindow begginingDateTimeWindow = new AddBegginingDateTimeWindow(ViewModel.begginings);
            begginingDateTimeWindow.ShowDialog();
        }

        private void AddPointOfInterest_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AddPointOfInterest_Executed(object sender, ExecutedRoutedEventArgs e) {
            AddPointsOfInterestWindow pointOfInterestWindow = new AddPointsOfInterestWindow(ViewModel.pointOfInterestDTOs);
            pointOfInterestWindow.ShowDialog();
        }

        private void AddPictures_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AddPictures_Executed(object sender, ExecutedRoutedEventArgs e) {
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

        /*private void DeleteDateTime_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void DeleteDateTime_Executed(object sender, ExecutedRoutedEventArgs e) {
            var button = (Button)sender;
            var beggining = (DateTime)button.DataContext;

            ViewModel.DeleteDateTime(beggining);
        }
        private void DeletePointOfInterest_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void DeletePointOfInterest_Executed(object sender, ExecutedRoutedEventArgs e) {
            var button = (Button)sender;
            var pointOfInterestDTO = (PointOfInterestDTO)button.DataContext;

            ViewModel.DeletePointOfInterest(pointOfInterestDTO);
        }*/   //EventHandler zna o kom buttonu se radi a Commanda ne zna, problem...
        private void deletePointOfInterestButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var pointOfInterestDTO = (PointOfInterestDTO)button.DataContext;

            ViewModel.DeletePointOfInterest(pointOfInterestDTO);
        }

        private void deleteDateTime_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var beggining = (DateTime)button.DataContext;

            ViewModel.DeleteDateTime(beggining);
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