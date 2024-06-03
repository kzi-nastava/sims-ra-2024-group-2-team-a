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
using System.Windows;
using System.Windows.Threading;

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
                    //if (!DemoMode) {
                        ViewCommand.RaiseCanExecuteChanged();
                        CancelCommand.RaiseCanExecuteChanged();
                    //}
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
            else if (!DemoMode)
                return _renovationService.CanBeCancelled(SelectedRenovation.Id);
            else
                return true;
        }

        public void ViewButton_Executed(object obj) {
            RenovationDescriptionWindow renovationDescriptionWindow = new RenovationDescriptionWindow(SelectedRenovation, false, false);
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

        private DispatcherTimer _timer;
        private int _currentFieldIndex;
        private List<AccommodationRenovationDTO> tempRenovations = new List<AccommodationRenovationDTO>();
        private bool DemoMode = false;

        private bool _cancelDemoClick;
        public bool CancelDemoClick {
            get {
                return _cancelDemoClick;
            }
            set {
                if (value != _cancelDemoClick) {
                    _cancelDemoClick = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _viewDemoClick;
        public bool ViewDemoClick {
            get {
                return _viewDemoClick;
            }
            set {
                if (value != _viewDemoClick) {
                    _viewDemoClick = value;
                    OnPropertyChanged();
                }
            }
        }
        public void StartDemo() {
            _currentFieldIndex = 0;
            DemoMode = true;

            foreach (var ren in Renovations) {
                tempRenovations.Add(ren);
            }
            DemoReset();


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private RenovationDescriptionWindow renovationDescriptionWindow;
        private void Timer_Tick(object sender, EventArgs e) {
            switch (_currentFieldIndex) {
                case 0:
                    SelectedRenovation = Renovations[0];
                    break;
                case 1:
                    SimulateButtonClickAppearance(2);
                    break;
                case 2:
                    renovationDescriptionWindow = new RenovationDescriptionWindow(SelectedRenovation, false, true);
                    renovationDescriptionWindow.ShowDialog();
                    break;
                case 3:
                    SimulateButtonClickAppearance(1);
                    break;
                case 4:
                    AccommodationRenovationDTO accRenDTO = Renovations[0];
                    Renovations.Clear();
                    accRenDTO.RenovationState = RenovationState.Cancelled;
                    Renovations.Add(accRenDTO);
                    break;
                case 5:
                    break;
                case 6:
                    DemoReset();
                    break;
            }

            _currentFieldIndex++;
            if (_currentFieldIndex > 6) {
                _currentFieldIndex = 0;
            }
        }
        private void DemoReset() {
            Renovations.Clear();
            AccommodationDTO tempAccDTO = new AccommodationDTO();
            tempAccDTO.Name = "Temp accommodation";
            Renovations.Add(new AccommodationRenovationDTO(1, new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 7),
                RenovationState.Pending, tempAccDTO, "This is only a demo"));
            SelectedRenovation = null;
        }

        //#5a8c6b
        private void SimulateButtonClickAppearance(int buttonNum) {
            if (buttonNum == 1)
                CancelDemoClick = true;
            if (buttonNum == 2)
                ViewDemoClick = true;

            DispatcherTimer revertTimer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            revertTimer.Tick += (s, e) => {
                if (buttonNum == 1)
                    CancelDemoClick = false;
                if (buttonNum == 2)
                    ViewDemoClick = false;
                revertTimer.Stop();
            };
            revertTimer.Start();
        }

        public void StopDemo() {
            _timer.Stop();
            SelectedRenovation = null;
            Renovations.Clear();
            DemoMode = false;

            foreach (var ren in tempRenovations) {
                Renovations.Add(ren);
            }
            tempRenovations.Clear();
        }

    }
}
