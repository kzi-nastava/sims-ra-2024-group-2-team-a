using BookingApp.Domain.Model;
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
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels
{
    public class AllRenovationsViewmodel: INotifyPropertyChanged
    {
        private readonly AccommodationRenovationService _renovationService = ServicesPool.GetService<AccommodationRenovationService>();

        private readonly AccommodationService _accommodationService = ServicesPool.GetService<AccommodationService>();

        private readonly User _user;
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
                    ViewCommand.RaiseCanExecuteChanged();
                    CancelCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public AndroidCommand ViewCommand { get; set; }
        public AndroidCommand CancelCommand { get; set; }
        public AllRenovationsViewmodel(User user) {

            Renovations = new ObservableCollection<AccommodationRenovationDTO>();
            _user = user;

            Update();
            ViewCommand = new AndroidCommand(ViewButton_Executed, ViewButton_CanBeExecuted);
            CancelCommand = new AndroidCommand(CancelButton_Executed, CancelButton_CanBeExecuted);
        }

        public void Update() {
            Renovations.Clear();    
            foreach (var renovation in _renovationService.GetByOwnerIdSorted(_user.Id)) {
                AccommodationRenovationDTO renovationDTO = new AccommodationRenovationDTO(renovation);
                renovationDTO.Accommodation = new AccommodationDTO(_accommodationService.GetById(renovationDTO.AccommodationId));

                Renovations.Add(renovationDTO);
            }
        }

        public bool ViewButton_CanBeExecuted(object obj) {
            return SelectedRenovation != null;
        }

        public bool CancelButton_CanBeExecuted(object obj) {
            if (SelectedRenovation == null)
                return false;
            else
                return _renovationService.CanBeCancelled(SelectedRenovation.Id);
        }

        public void ViewButton_Executed(object obj) {
            RenovationDescriptionWindow renovationDescriptionWindow = new RenovationDescriptionWindow(SelectedRenovation, false);
            renovationDescriptionWindow.ShowDialog();
        }

        public void CancelButton_Executed(object obj) {
            if (SelectedRenovation == null) {
                return;
            }

            _renovationService.CancelRenovation(SelectedRenovation.Id);
            this.Update();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
