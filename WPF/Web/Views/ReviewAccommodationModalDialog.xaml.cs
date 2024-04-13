using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.Web.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        }

        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }
    }
}
