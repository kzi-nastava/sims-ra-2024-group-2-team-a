using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class AddTourViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _userId;

        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        private readonly PointOfInterestService _pointOfInterestService = ServicesPool.GetService<PointOfInterestService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();

        public TourDTO tourDTO { get; set; }

        public LocationDTO selectedLocationDTO { get; set; }
        public LanguageDTO selectedLanguageDTO { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public ObservableCollection<DateTime> begginings { get; set; }

        private ObservableCollection<string> _imagePaths = new ObservableCollection<string>();
        public ObservableCollection<string> ImagePaths {
            get {
                return _imagePaths;
            }
            set {
                if (_imagePaths != value) {
                    _imagePaths = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _selectedImageIndex;
        public int SelectedImageIndex {
            get {
                return _selectedImageIndex;
            }
            set {
                if (_selectedImageIndex != value) {
                    _selectedImageIndex = value;
                    OnPropertyChanged();
                    UpdateImage();
                }
            }
        }

        private string _currentImagePath;
        public string CurrentImagePath {
            get {
                return _currentImagePath;
            }
            set {
                if (_currentImagePath != value) {
                    _currentImagePath = value;
                    OnPropertyChanged();
                }
            }
        }
        public AddTourViewModel(TourDTO tDTO, int userId) {
            _userId = userId;
            Load();
            if (tDTO == null)
                tourDTO = new TourDTO();
            else {
                tourDTO = tDTO;
                LoadSelectedLanuageLocation();
            }
            SelectedImageIndex = -1;

        }
        public void AddTour() {
            foreach (var beggining in begginings) {
                SetTour(beggining);
                SaveTour();
            }
        }
        public void DeleteDateTime(DateTime beggining) {
            begginings.Remove(beggining);
        }
        public void DeletePointOfInterest(PointOfInterestDTO pointOfInterestDTO) {
            pointOfInterestDTOs.Remove(pointOfInterestDTO);
        }

        public bool CheckValidation() {
            if (pointOfInterestDTOs.Count >= 2)
                return true;
            else
                return false;
        }

        private void Load() {
            locationDTOs = new ObservableCollection<LocationDTO>();
            languageDTOs = new ObservableCollection<LanguageDTO>();
            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();
            begginings = new ObservableCollection<DateTime>();


            foreach (var lan in _languageService.GetAll()) {
                languageDTOs.Add(new LanguageDTO(lan));
            }
            foreach (var loc in _locationService.GetAll()) {
                locationDTOs.Add(new LocationDTO(loc));
            }
        }

        private void SetTour(DateTime beggining) {
            tourDTO.LocationId = selectedLocationDTO.Id;
            tourDTO.LanguageId = selectedLanguageDTO.Id;
            tourDTO.CurrentTouristNumber = 0;
            tourDTO.setBeggining(beggining);
            tourDTO.GuideId = _userId;

            foreach (var imagePath in ImagePaths)
                tourDTO.ProfilePictures.Add(imagePath);
        }

        private void SaveTour() {
            Tour tour = _tourService.Save(tourDTO.ToModelNoId());
            foreach (var pDTO in pointOfInterestDTOs) {
                pDTO.TourId = tour.Id;
                _pointOfInterestService.Save(pDTO.ToModelNoId());
            }
        }
        /*public void PickPhotos(List<string> absolutePaths) {

            // Convert absolute paths to relative paths
            string basePath = Directory.GetCurrentDirectory(); // Use application directory as base
            foreach (string absolutePath in absolutePaths) {
                string relativePath = GetRelativePath(basePath, absolutePath);
                tourDTO.ProfilePictures.Add(relativePath);
            }
        }*/
        /*private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }*/
        private void LoadSelectedLanuageLocation() {
            if(tourDTO.LanguageId == 0) {
                selectedLocationDTO = new LocationDTO(_locationService.GetById(tourDTO.LocationId));
            }
            else {
                selectedLanguageDTO = new LanguageDTO(_languageService.GetById(tourDTO.LanguageId));
            }
        }
        public void AddImage() {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files |*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == true) {
                ImagePaths.Add(openFileDialog.FileName);
                if (SelectedImageIndex == -1)
                    SelectedImageIndex = 0;

                UpdateImage();
            }
        }

        public void UpdateImage() {
            if (_selectedImageIndex != -1)
                CurrentImagePath = ImagePaths[SelectedImageIndex];
            else {
                CurrentImagePath = null;
            }
        }

        public void DeleteImage() {
            if (SelectedImageIndex >= 0 && SelectedImageIndex < ImagePaths.Count) {
                ImagePaths.RemoveAt(SelectedImageIndex);
                if (SelectedImageIndex >= ImagePaths.Count)
                    SelectedImageIndex = ImagePaths.Count - 1;

                UpdateImage();
            }
        }

        public bool CanNavigatePrevious() {
            return SelectedImageIndex > 0;
        }

        public void NavigatePreviousImage() {
            SelectedImageIndex--;
        }

        public bool CanNavigateNext() {
            return SelectedImageIndex < ImagePaths.Count - 1;
        }

        public void NavigateNextImage() {
            SelectedImageIndex++;
        }

    }
}
