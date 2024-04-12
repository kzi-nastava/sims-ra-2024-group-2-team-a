using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AddAccomodationPage.xaml
    /// </summary>
    public partial class AddAccommodationPage : Page {
        public Frame mainFrame;

        private readonly User _user;

        private AccommodationService accommodationService = new AccommodationService();
        private LocationService locationService = new LocationService();
        public AccommodationDTO AccommodationDTO { get; set; }
        public LocationDTO SelectedLocationDTO { get; set; }

        public ObservableCollection<LocationDTO> LocationDTOs { get; set; }

        public AddAccommodationPage(Frame mainFrame, User user) {
            InitializeComponent();
            this.mainFrame = mainFrame;
            DataContext = this;

            _user = user;
            AccommodationDTO = new AccommodationDTO();
            AccommodationDTO.OwnerId = _user.Id;
            LocationDTOs = new ObservableCollection<LocationDTO>();

            foreach (var loc in locationService.GetAll()) {
                LocationDTOs.Add(new LocationDTO(loc));
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Your progress will be lost, are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) {
                mainFrame.GoBack();
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            Accommodation acc = AccommodationDTO.ToAccommodation();

            acc.LocationId = SelectedLocationDTO.Id;
            accommodationService.Save(acc);

            mainFrame.Content = new AccommodationPage(mainFrame, _user);
        }

        private void SelectImages_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Strahinja\Desktop\ProjekatSims\sims-ra-2024-group-2-team-a\Resources\Images\Accomodations\";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == false)
                return;

            string basePath = Directory.GetCurrentDirectory();
            foreach (string absolutePath in openFileDialog.FileNames) {
                string relativePath = GetRelativePath(basePath, absolutePath);
                AccommodationDTO.ProfilePictures.Add(relativePath);
            }
        }
        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private void ViewImages_Click(object sender, RoutedEventArgs e) {
            if (AccommodationDTO.ProfilePictures.Count == 0) {
                MessageBox.Show("Please select an image first","",MessageBoxButton.OK);
            }
            else {
                ViewSelectedImagesWindow viewSelectedImagesWindow = new ViewSelectedImagesWindow(AccommodationDTO);
                viewSelectedImagesWindow.ShowDialog();
            }
        }
    }
}
