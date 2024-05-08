using BookingApp.Services;
using BookingApp.WPF.Android.Views;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.Android.ViewModels {
    public class AccommodationRenovationViewmodel: INotifyPropertyChanged {

        private readonly AccommodationRenovationService _renovationService;

        public AccommodationDTO AccommodationDTO { get; set; }

        public DateTime? _startDate;
        public DateTime? StartDate {
            get {
                return _startDate;
            }
            set {
                if (value != _startDate) {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? _endDate;
        public DateTime? EndDate {
            get {
                return _endDate;
            }
            set {
                if (value != _endDate) {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration {
            get {
                return _duration;
            }
            set {
                if (value != _duration) {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<AccommodationRenovationDTO> Renovations { get; set; }

        private AccommodationRenovationDTO? _selectedRenovation;
        public AccommodationRenovationDTO? SelectedRenovation {
            get {
                return _selectedRenovation;
            }
            set {
                if (value != _selectedRenovation) {
                    _selectedRenovation = value;
                    OnPropertyChanged();
                    AcceptCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private readonly Frame _mainFrame;
        public AndroidCommand AcceptCommand { get; set; }
        public AndroidCommand DeclineCommand { get; set; }
        public AccommodationRenovationViewmodel(AccommodationDTO accommodationDTO, Frame mainFrame) {
            _renovationService = ServicesPool.GetService<AccommodationRenovationService>();
            StartDate = null;
            EndDate = null;
            SelectedRenovation = null;
            Renovations = new ObservableCollection<AccommodationRenovationDTO>();
            
            _mainFrame = mainFrame;
            AccommodationDTO = accommodationDTO;
            AcceptCommand = new AndroidCommand(AcceptButton_Execute, AcceptButton_CanExecute);
            DeclineCommand = new AndroidCommand(DeclineButton_Execute,DeclineButton_CanExecute);
        }
        public void SearchButton() {
            Renovations.Clear();

            foreach (var ren in _renovationService.GetAvaliableRenovationDates(AccommodationDTO.Id,
                DateOnly.FromDateTime(StartDate ?? DateTime.MinValue), DateOnly.FromDateTime(EndDate ?? DateTime.MinValue), Duration)) {

                AccommodationRenovationDTO accRenDTO = new AccommodationRenovationDTO(ren);
                accRenDTO.Accommodation = AccommodationDTO;

                Renovations.Add(accRenDTO);
            }
        }

        public void AcceptButton_Execute(object obj) {
            RenovationDescriptionWindow renovationDescriptionWindow = new RenovationDescriptionWindow(SelectedRenovation, true);
            renovationDescriptionWindow.ShowDialog();
            _mainFrame.GoBack();
        }

        public bool AcceptButton_CanExecute(object obj) {
            return SelectedRenovation != null;
        }

        public void DeclineButton_Execute(object obj) {
            _mainFrame.GoBack();
        }

        public bool DeclineButton_CanExecute(object obj) {
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
