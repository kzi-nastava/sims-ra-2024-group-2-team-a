﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for FollowLiveTourPage.xaml
    /// </summary>
    public partial class FollowLiveTourPage : Page {
        private int _userId;


        private readonly LocationService _locationService = new LocationService();
        private readonly LanguageService _languageService = new LanguageService();
        private readonly TourService _tourService = new TourService();

        private TourFilterDTO _filterDTO;
        public TourDTO tourDTO { get; set; }
        public LocationDTO selectedLocationDTO { get; set; }
        public LanguageDTO selectedLanguageDTO { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }
        public FollowLiveTourPage(int userId) {
            InitializeComponent();
            _userId = userId;
            DataContext = this;

            Load();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e) {
            _filterDTO = SetFilter();

            tourDTOs.Clear();
            foreach (var tour in _tourService.GetFilteredLive(_filterDTO, _userId)) {
                TourDTO temp = new TourDTO(tour);
                setLocationLanguage(temp, tour);
                tourDTOs.Add(temp);
            }

            itemsControlLiveTours.ItemsSource = tourDTOs;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e) {
            textBoxName.Text = string.Empty;
            textBoxTouristNumber.Text = string.Empty;
            textBoxDuration.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
        }
        private void Load() {
            locationDTOs = new ObservableCollection<LocationDTO>();
            languageDTOs = new ObservableCollection<LanguageDTO>();

            locationDTOs.Add(new LocationDTO());
            languageDTOs.Add(new LanguageDTO());

            foreach (var lan in _languageService.GetAll()) {
                languageDTOs.Add(new LanguageDTO(lan));
            }
            foreach (var loc in _locationService.GetAll()) {
                locationDTOs.Add(new LocationDTO(loc));
            }
            LoadLive();
        }
        private void LoadLive() {
            tourDTOs = new ObservableCollection<TourDTO>();
            foreach (var tour in _tourService.GetLive(_userId)) {
                TourDTO tempTourDTO = new TourDTO(tour);
                setLocationLanguage(tempTourDTO, tour);
                tourDTOs.Add(tempTourDTO);
            }
        }
        private void setLocationLanguage(TourDTO tempTourDTO, Tour tour) {
            Location location = _locationService.GetById(tour.LocationId);
            string city = location.City;
            string country = location.Country;
            tempTourDTO.setLocationTemplate(city, country);
            tempTourDTO.LanguageTemplate = _languageService.GetById(tour.LanguageId).Name;
        }

        private TourFilterDTO SetFilter() {
            string name = textBoxName.Text;
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            if (location == null) {
                location = new LocationDTO();
            }
            LanguageDTO language = (LanguageDTO)comboBoxLanguage.SelectedItem;
            if (language == null) {
                language = new LanguageDTO();
            }
            if (!int.TryParse(textBoxTouristNumber.Text, out int touristsNumber)) {
                textBoxTouristNumber.Text = string.Empty;
            }
            if (!int.TryParse(textBoxDuration.Text, out int duration)) {
                textBoxDuration.Text = string.Empty;
            }
            return  new TourFilterDTO(name, duration, touristsNumber, location, language, DateTime.MinValue);
        }
    }
}
