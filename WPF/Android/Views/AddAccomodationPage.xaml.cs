using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Validation;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AddAccomodationPage.xaml
    /// </summary>
    public partial class AddAccommodationPage : Page, IDemo {
        public Frame mainFrame;

        private readonly User _user;

        private AccommodationService accommodationService = ServicesPool.GetService<AccommodationService>();
        private LocationService locationService = ServicesPool.GetService<LocationService>();
        public AccommodationDTO AccommodationDTO { get; set; }
        public LocationDTO SelectedLocationDTO { get; set; }
        public ObservableCollection<LocationDTO> LocationDTOs { get; set; }

        public AddAccommodationPage(Frame mainFrame, User user, LocationDTO presetLocation) {
            InitializeComponent();
            this.mainFrame = mainFrame;
            DataContext = this;

            _user = user;
            AccommodationDTO = new AccommodationDTO();
            AccommodationDTO.MinReservationDays = 1;
            AccommodationDTO.MaxGuestNumber = 1;
            AccommodationDTO.LastCancellationDay = 1;
            AccommodationDTO.OwnerId = _user.Id;
            LocationDTOs = new ObservableCollection<LocationDTO>();

            foreach (var loc in locationService.GetAll()) {
                LocationDTO locDTO = new LocationDTO(loc);
                LocationDTOs.Add(locDTO);
                if (presetLocation != null && presetLocation.Id == loc.Id)
                    SelectedLocationDTO = locDTO;
                    
            }

            nameTextbox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
        }

        private void Decline_Click(object sender, RoutedEventArgs e) {
            AndroidYesNoDialog dialog = new AndroidYesNoDialog("Your progress will be lost, are you sure?");
            bool? result = dialog.ShowDialog();

            if (result == false) {
                return;
            }
            
            mainFrame.GoBack();
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

            if (AccommodationDTO.ProfilePictures.Count != 0)
                ViewButton.IsEnabled = true;
                importLabel.Content = "";
        }
        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private void ViewImages_Click(object sender, RoutedEventArgs e) {
            if (AccommodationDTO.ProfilePictures.Count == 0) {
                ViewButton.IsEnabled = false;
                importLabel.Content = "No images selected!";
            }
            else {
                ViewSelectedImagesWindow viewSelectedImagesWindow = new ViewSelectedImagesWindow(AccommodationDTO);
                viewSelectedImagesWindow.ShowDialog();
            }
        }

        private DispatcherTimer _timer;
        private int _currentFieldIndex;
        private AccommodationDTO tempAccommodationDTO;
        private LocationDTO tempSelectedLocation;
        public void StartDemo() {
            _currentFieldIndex = 0;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            tempAccommodationDTO = new AccommodationDTO(AccommodationDTO);
            if(SelectedLocationDTO != null)
                tempSelectedLocation = new LocationDTO(SelectedLocationDTO);
        }
        private void Timer_Tick(object sender, EventArgs e) {
            switch (_currentFieldIndex) {
                case 0:
                    DemoNameInput();
                    break;
                case 2:
                    SelectedLocationDTO = LocationDTOs[0];
                    comboBox.SelectedItem = SelectedLocationDTO;
                    break;
                case 3:
                    AccommodationDTO.MaxGuestNumber = 2;
                    break;
                case 4:
                    AccommodationDTO.MinReservationDays = 5;
                    break;
                case 5:
                    AccommodationDTO.LastCancellationDay = 2;
                    break;
                case 6:
                    AccommodationDTO.Type = AccommodationType.house;
                    break;
                case 7:
                    SimulateButtonClickAppearance(ImportButton);
                    ConfirmButton.IsEnabled = true;
                    break;
                case 8:
                    SimulateButtonClickAppearance(ConfirmButton);
                    ConfirmButton.IsEnabled = false;
                    break;
                case 9:
                    DemoReset();
                    break;
            }

            _currentFieldIndex++;
            if (_currentFieldIndex > 9) 
            {
                _currentFieldIndex = 0;
            }
        }

        private void DemoNameInput() {
            DispatcherTimer timer2 = new DispatcherTimer();
            int index = 0;
            string input = "Villa Kamenica";
            timer2.Interval = TimeSpan.FromSeconds(0.05);
            timer2.Tick += (sender,e) => {
                AccommodationDTO.Name += input[index++];
                if (index == input.Length) {
                    timer2.Stop();
                    return;
                }
            };
            timer2.Start();
        }

        private void DemoReset() {
            AccommodationDTO.Name = "";
            AccommodationDTO.MaxGuestNumber = 1;
            AccommodationDTO.MinReservationDays = 1;
            AccommodationDTO.LastCancellationDay = 1;
            AccommodationDTO.Type = AccommodationType.apartment;

            SelectedLocationDTO = null;
            comboBox.SelectedItem = SelectedLocationDTO;
        }
        private void SimulateButtonClickAppearance(Button button) {
            var brushConverter = new BrushConverter();
            button.Background = (Brush)brushConverter.ConvertFromString("#5a8c6b");
            button.BorderBrush = Brushes.DarkGray; 

            DispatcherTimer revertTimer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            revertTimer.Tick += (s, e) => {
                button.ClearValue(Button.BackgroundProperty);
                button.ClearValue(Button.BorderBrushProperty); 
                revertTimer.Stop();
            };
            revertTimer.Start();
        }

        public void StopDemo() {
            AccommodationDTO.Name = tempAccommodationDTO.Name;
            AccommodationDTO.MaxGuestNumber = tempAccommodationDTO.MaxGuestNumber;
            AccommodationDTO.MinReservationDays = tempAccommodationDTO.MinReservationDays;
            AccommodationDTO.LastCancellationDay = tempAccommodationDTO.LastCancellationDay;
            AccommodationDTO.Type = tempAccommodationDTO.Type;

            SelectedLocationDTO = tempSelectedLocation;
            comboBox.SelectedItem = SelectedLocationDTO;

            _timer.Stop();
        }
    }
}
