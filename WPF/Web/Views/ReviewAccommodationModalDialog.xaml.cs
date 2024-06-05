using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ReviewAccommodationModalDialog.xaml
    /// </summary>
    public partial class ReviewAccommodationModalDialog : UserControl {

        private ReviewAccommodationModalDialogViewModel ViewModel { get; set; }

        private ReservationsPage _parentPage;

        public ReviewAccommodationModalDialog(ReservationsPage parentPage, AccommodationReservationDTO reservation) {
            InitializeComponent();
            _parentPage = parentPage;
            ViewModel = new ReviewAccommodationModalDialogViewModel(reservation);
            DataContext = ViewModel;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            ViewModel.GradeOwner();
            _parentPage.Update();
            App.NotificationService.ShowSuccess("Accommodation successfully reviewed!");
            _parentPage.CloseModalDialog();
        }

        private void CheckBoxRequiresRenovationUnchecked(object sender, RoutedEventArgs e) {
            textBoxRenovationComment.Text = "";
            comboBoxRenovationImportance.SelectedIndex = 0;
        }

        private void ButtonUploadClick(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetFullPath(@"..\..\..\Resources\Images\AccommodationsInside");
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            openFileDialog.ShowDialog();

            string basePath = Directory.GetCurrentDirectory();
            foreach (string fileName in openFileDialog.FileNames) {
                string relativePath = GetRelativePath(basePath, fileName);
                ViewModel.Review.AccommodationPhotos.Add(relativePath);
            }

            SetPicturePreview();
        }

        private void SetPicturePreview() {
            if(ViewModel.Review.AccommodationPhotos.Count > 0) {
                picturesPreview.Visibility = Visibility.Visible;
                ViewModel.SelectedAccommodationPicture = ViewModel.Review.AccommodationPhotos[0];
            }
            else {
                picturesPreview.Visibility = Visibility.Collapsed;
            }

            if(ViewModel.Review.AccommodationPhotos.Count == 1) {
                buttonLeft.IsEnabled = false;
                buttonRight.IsEnabled = false;
            }
            else {
                buttonLeft.IsEnabled = false;
                buttonRight.IsEnabled = true;
            }
        }

        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private void ButtonLeftClick(object sender, RoutedEventArgs e) {
            ViewModel.ChangePictureLeft();

            if (ViewModel.PicturesIndex == 0) {
                buttonLeft.IsEnabled = false;
                buttonRight.IsEnabled = true;
            }
        }

        private void ButtonRightClick(object sender, RoutedEventArgs e) {
            ViewModel.ChangePictureRight();

            if (ViewModel.PicturesIndex == ViewModel.MaxPictureIndex) {
                buttonRight.IsEnabled = false;
                buttonLeft.IsEnabled = true;
            }
        }

        private void ButtonRemoveClick(object sender, RoutedEventArgs e) {
            ViewModel.RemovePicture();
            SetPicturePreview();
        }
    }
}
