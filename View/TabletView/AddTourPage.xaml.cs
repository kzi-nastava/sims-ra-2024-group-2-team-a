using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for AddTour.xaml
    /// </summary>
    public partial class AddTourPage : Page {
        private Frame _mainFrame;
        private int _userId;
        private readonly User _user;
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly PointOfInterestRepository _pointOfInterestRepository;

        public TourDTO tourDTO { get; set; }
        public LocationDTO selectedLocationDTO { get; set; }
        public LanguageDTO selectedLanguageDTO { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public ObservableCollection<DateTime> begginings { get; set; }

        private string _dateText;
        private string _timeText;
        public AddTourPage(Frame mainF, int userId) {
            InitializeComponent();
            _mainFrame = mainF;
            _userId = userId;

            DataContext = this;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();
            _pointOfInterestRepository = new PointOfInterestRepository();

            Load();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e) {
            foreach(var beggining in begginings) {
                SetTour(beggining);
                SaveTour();
            }
            MessageBox.Show("Tour Added Succesfully", "Confirmed", MessageBoxButton.OK, MessageBoxImage.Information);
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
        }


        private void addPointOfInterestButton_Click(object sender, RoutedEventArgs e) {
            AddPointsOfInterestWindow pointOfInterestWindow = new AddPointsOfInterestWindow(pointOfInterestDTOs);
            pointOfInterestWindow.ShowDialog();
            CheckValidation();
        }

        private void deletePointOfInterestButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var pointOfInterestDTO = (PointOfInterestDTO)button.DataContext;

            pointOfInterestDTOs.Remove(pointOfInterestDTO);
            CheckValidation();
        }


        private void addDateTime_Click(object sender, RoutedEventArgs e) {
            AddBegginingDateTimeWindow begginingDateTimeWindow = new AddBegginingDateTimeWindow(begginings);
            begginingDateTimeWindow.ShowDialog();
        }

        private void deleteDateTime_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var beggining = (DateTime)button.DataContext;

            begginings.Remove(beggining);
        }

        private void pickPhotosButton_Click(object sender, RoutedEventArgs e) {
            List<string> absolutePaths = new List<string>();
            OpenFileDialog openFileDialog = SetOpenFile();
            if (openFileDialog.ShowDialog() == true) {
                // Get absolute paths of selected images
                foreach (string filename in openFileDialog.FileNames) {
                    absolutePaths.Add(filename);
                }

                // Convert absolute paths to relative paths
                string basePath = Directory.GetCurrentDirectory(); // Use application directory as base
                foreach (string absolutePath in absolutePaths) {
                    string relativePath = GetRelativePath(basePath, absolutePath);
                    tourDTO.ProfilePictures.Add(relativePath);
                }
            }
        }
        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private void CheckValidation() {
            if (pointOfInterestDTOs.Count >= 2)
                confirmButton.IsEnabled = true;
            else
                confirmButton.IsEnabled = false;
        }

        private void Load() {
            tourDTO = new TourDTO();
            locationDTOs = new ObservableCollection<LocationDTO>();
            languageDTOs = new ObservableCollection<LanguageDTO>();
            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();
            begginings = new ObservableCollection<DateTime>();


            foreach (var lan in _languageRepository.GetAll()) {
                languageDTOs.Add(new LanguageDTO(lan));
            }
            foreach (var loc in _locationRepository.GetAll()) {
                locationDTOs.Add(new LocationDTO(loc));
            }
        }

        private void SetTour(DateTime beggining) {
            tourDTO.LocationId = selectedLocationDTO.Id;
            tourDTO.LanguageId = selectedLanguageDTO.Id;
            tourDTO.CurrentTouristNumber = 0;
            tourDTO.setBeggining(beggining);
            tourDTO.GuideId = _userId;
        }

        private void SaveTour() {
            Tour tour = _tourRepository.Save(tourDTO.ToModelNoId());
            foreach (var pDTO in pointOfInterestDTOs) {
                pDTO.TourId = tour.Id;
                _pointOfInterestRepository.Save(pDTO.ToModelNoId());
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