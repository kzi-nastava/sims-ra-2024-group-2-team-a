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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BookingApp.WPF.Android.ViewModels {
    public class AccommodationRenovationViewmodel: INotifyPropertyChanged, IDemo {

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
            RenovationDescriptionWindow renovationDescriptionWindow = new RenovationDescriptionWindow(SelectedRenovation, true, false);
            renovationDescriptionWindow.ShowDialog();
            _mainFrame.GoBack();
        }

        public bool AcceptButton_CanExecute(object obj) {
            return SelectedRenovation != null;
        }

        public void DeclineButton_Execute(object obj) {
            AndroidYesNoDialog dialog = new AndroidYesNoDialog("Your progress will be lost, are you sure?");
            bool? result = dialog.ShowDialog();

            if (result == false) {
                return;
            }

            _mainFrame.GoBack();
        }

        public bool DeclineButton_CanExecute(object obj) {
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //DEMO

        private DispatcherTimer _timer;
        private int _currentFieldIndex;
        private DateTime? tempStartDate;
        private DateTime? tempEndDate;
        private int tempDuration;

        private bool _searchDemoClick;
        public bool SearchDemoClick {
            get {
                return _searchDemoClick;
            }
            set {
                if (value != _searchDemoClick) {
                    _searchDemoClick = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _confirmDemoClick;
        public bool ConfirmDemoClick {
            get {
                return _confirmDemoClick;
            }
            set {
                if (value != _confirmDemoClick) {
                    _confirmDemoClick = value;
                    OnPropertyChanged();
                }
            }
        }
        public void StartDemo() {
            _currentFieldIndex = 0;
            tempStartDate = StartDate;
            tempEndDate = EndDate;
            tempDuration = Duration;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start(); 
        }
        private void Timer_Tick(object sender, EventArgs e) {
            switch (_currentFieldIndex) {
                case 0:
                    StartDate = new DateTime(1990,1,1);
                    break;
                case 1:
                    EndDate = new DateTime(1991, 1, 1);
                    break;
                case 2:
                    Duration = 2;
                    break;
                case 3:
                    SimulateButtonClickAppearance(1);
                    break;
                case 4:
                    SearchButton();
                    break;
                case 5:
                    SelectedRenovation = Renovations[0];
                    break;
                case 6:
                    SimulateButtonClickAppearance(2);
                    break;
                case 7:
                    DemoReset();
                    break;
            }

            _currentFieldIndex++;
            if (_currentFieldIndex > 7) {
                _currentFieldIndex = 0;
            }
        }
        private void DemoReset() {
            StartDate = null;
            EndDate = null;
            Duration = 1;
            Renovations.Clear();
            SelectedRenovation = null;
        }

        //#5a8c6b
        private void SimulateButtonClickAppearance(int buttonNum) {
            if(buttonNum == 1)
                SearchDemoClick = true;
            if (buttonNum == 2)
                ConfirmDemoClick = true;

            DispatcherTimer revertTimer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            revertTimer.Tick += (s, e) => {
                if (buttonNum == 1)
                    SearchDemoClick = false;
                if (buttonNum == 2)
                    ConfirmDemoClick = false;
                revertTimer.Stop();
            };
            revertTimer.Start();
        }

        public void StopDemo() {
            StartDate = tempStartDate;
            EndDate = tempEndDate;
            Duration = tempDuration;
            _timer.Stop();
        }
    }
}
