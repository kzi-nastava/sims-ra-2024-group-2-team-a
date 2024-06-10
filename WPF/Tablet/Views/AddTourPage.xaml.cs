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

        public AddTourPage(TourDTO tDTO, Frame mainF, int userId) {
            ViewModel = new AddTourViewModel(tDTO, userId);
            InitializeComponent();
            _mainFrame = mainF;
            _userId = userId;
            DataContext = ViewModel;
            comboBoxLocation.SelectedIndex = ViewModel.tourDTO.LocationId - 1;
            comboBoxLanguage.SelectedIndex = ViewModel.tourDTO.LanguageId - 1;
        }

        private void Reset_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Reset_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.Content = new AddTourPage(null, _mainFrame, _userId);
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
            _mainFrame.Content = new AddTourPage(null, _mainFrame, _userId);
        }

        private void AddDateTime_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AddDateTime_Executed(object sender, ExecutedRoutedEventArgs e) {
            AddBegginingDateTimeWindow begginingDateTimeWindow = new AddBegginingDateTimeWindow(ViewModel.begginings);
            begginingDateTimeWindow.ShowDialog();
            ViewModel.SetDateTimeValid();
        }

        private void AddPointOfInterest_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AddPointOfInterest_Executed(object sender, ExecutedRoutedEventArgs e) {
            AddPointsOfInterestWindow pointOfInterestWindow = new AddPointsOfInterestWindow(ViewModel.pointOfInterestDTOs);
            pointOfInterestWindow.ShowDialog();
            ViewModel.SetKeypointValid();
        }

        private void AddPictures_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AddPictures_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.AddImage();
            ViewModel.SetPhotosValid();
        }
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

        private void DeletePictures_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (ViewModel.SelectedImageIndex == -1)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void DeletePictures_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.DeleteImage();
            ViewModel.SetPhotosValid();

        }

        private void Previous_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if(ViewModel.CanNavigatePrevious()) {
                e.CanExecute= true;
            }
            else {
                e.CanExecute = false;
            }
        }

        private void Previous_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.NavigatePreviousImage();
        }

        private void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (ViewModel.CanNavigateNext()) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
        }

        private void Next_Executed(object sender, ExecutedRoutedEventArgs e) {
            ViewModel.NavigateNextImage();
        }
    }
}