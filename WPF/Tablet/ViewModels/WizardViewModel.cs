using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class WizardViewModel : INotifyPropertyChanged{
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly WizardPicsService _wizardPicsService = ServicesPool.GetService<WizardPicsService>();
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
        private string _previous;
        public string previous {
            get {
                return _previous;
            }
            set {
                if (_previous != value) {
                    _previous = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _next;
        public string next {
            get {
                return _next;
            }
            set {
                if (_next != value) {
                    _next = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool isAbleToEnd { get; set; } = true;
        public WizardViewModel(int wizardId) {
            foreach(var i in _wizardPicsService.GetImages(wizardId)) {
                ImagePaths.Add(i);
            }
            SelectedImageIndex = 0;
            UpdateImage();
        }
        public void UpdateImage() {
            CurrentImagePath = ImagePaths[SelectedImageIndex];
        }

        public bool CanNavigatePrevious() {
            if (SelectedImageIndex == 0) {
                previous = "Skip";
                isAbleToEnd = true;
            }
            else { 
                previous = "Previous";
                isAbleToEnd = false;
            }
            return SelectedImageIndex > -1;
        }

        public void NavigatePreviousImage() {
            SelectedImageIndex--;
        }

        public bool CanNavigateNext() {
            if (SelectedImageIndex == ImagePaths.Count - 1) {
                next = "Finish";
                isAbleToEnd = true;
            }
            else {
                next = "Next";
                isAbleToEnd = false;
            }
            return SelectedImageIndex < ImagePaths.Count;
        }

        public void NavigateNextImage() {
            SelectedImageIndex++;
        }
    }
}
