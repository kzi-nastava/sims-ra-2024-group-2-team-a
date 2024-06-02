using BookingApp.WPF.DTO;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BookingApp.Domain.Model;
using BookingApp.Services;
using System.Windows.Input;
using QuestPDF.Infrastructure;
using BookingApp.WPF.Utils.Reports.Tourist;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class ReportWindowViewModel : INotifyPropertyChanged {
        private readonly UserService _userService = ServicesPool.GetService<UserService>();
        private readonly PassengerService _passengerService = ServicesPool.GetService<PassengerService>();
        private readonly PointOfInterestService _pointOfInterestService = ServicesPool.GetService<PointOfInterestService>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _filePath;
        public string FilePath {
            get {
                return _filePath;
            }
            set {
                if (_filePath != value) {
                    _filePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEnabled = false;
        public bool GenerateEnabled {
            get {
                return _isEnabled;
            }
            set {
                if (value != _isEnabled) {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public int UserId { get; set; }
        public TourDTO Tour { get; set; }
        public ICommand GenerateReportCommand { get; set; }
        public ICommand ChooseFileDestinationCommand { get; set; }

        public ReportWindowViewModel(int userId, TourDTO tour) {
            UserId = userId;
            Tour = tour;

            GenerateReportCommand = new RelayCommand(GenerateReport, CanGenerate);
            ChooseFileDestinationCommand = new RelayCommand(BrowseFilePath, () => true);
        }

        private List<PassengerDTO> LoadPassengers() {
            List<PassengerDTO> passengers = new List<PassengerDTO>();

            foreach (var passenger in _passengerService.GetByTourAndTourist(Tour.Id, UserId))
                passengers.Add(new PassengerDTO(passenger));

            return passengers;
        }

        private List<PointOfInterestDTO> LoadPointsOfInterest() {
            List<PointOfInterestDTO> pointsOfInterest = new List<PointOfInterestDTO>();

            foreach (var poi in _pointOfInterestService.GetAllByTourId(Tour.Id))
                pointsOfInterest.Add(new PointOfInterestDTO(poi));

            return pointsOfInterest;
        }

        private bool CanGenerate() {
            return _isEnabled;
        }

        private void GenerateReport(object parameter) {
            QuestPDF.Settings.License = LicenseType.Community;
            var report = new TouristReportGenerator(DateTime.Now, _userService.GetById(UserId).Username, Tour, LoadPassengers(), LoadPointsOfInterest());
            report.GeneratePdf($"{FilePath}/TourReservationReport{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf");

            // Optionally, notify the user that the report has been generated
            // For example, use a MessageBox or another UI element in the View
        }

        private void BrowseFilePath(object parameter) {
            using (var dialog = new FolderBrowserDialog()) {
                dialog.Description = "Select pdf generation folder";
                dialog.ShowNewFolderButton = true;
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath)) {
                    FilePath = dialog.SelectedPath;
                    FilePath = FilePath.Replace('\\', '/');
                    GenerateEnabled = true;
                }
            }
        }
    }   
}
