﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.Tablet.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class AddTourViewModel {
        private int _userId;

        private readonly TourService _tourService = new TourService();
        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly LocationService _locationService = new LocationService();
        private readonly LanguageService _languageService = new LanguageService();

        public TourDTO tourDTO { get; set; }
        public LocationDTO selectedLocationDTO { get; set; }
        public LanguageDTO selectedLanguageDTO { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public ObservableCollection<DateTime> begginings { get; set; }
        public AddTourViewModel(int userId) {
            _userId = userId;
            Load();
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
        public bool DeletePointOfInterest(PointOfInterestDTO pointOfInterestDTO) {
            pointOfInterestDTOs.Remove(pointOfInterestDTO);
            return CheckValidation();
        }

        public bool CheckValidation() {
            if (pointOfInterestDTOs.Count >= 2)
                return true;
            else
                return false;
        }

        private void Load() {
            tourDTO = new TourDTO();
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
        }

        private void SaveTour() {
            Tour tour = _tourService.Save(tourDTO.ToModelNoId());
            foreach (var pDTO in pointOfInterestDTOs) {
                pDTO.TourId = tour.Id;
                _pointOfInterestService.Save(pDTO.ToModelNoId());
            }
        }
        public void PickPhotos(List<string> absolutePaths) {

            // Convert absolute paths to relative paths
            string basePath = Directory.GetCurrentDirectory(); // Use application directory as base
            foreach (string absolutePath in absolutePaths) {
                string relativePath = GetRelativePath(basePath, absolutePath);
                tourDTO.ProfilePictures.Add(relativePath);
            }
        }
        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }
    }
}