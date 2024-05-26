using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TourRatingWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly TourReviewService _tourReviewService = ServicesPool.GetService<TourReviewService>();
        public int UserId { get; set; }
        public TourDTO SelectedTour { get; set; }

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

        public ICommand AddImageCommand { get; private set; }
        public ICommand DeleteImageCommand { get; private set; }
        public ICommand PreviousImageCommand { get; private set; }
        public ICommand NextImageCommand { get; private set; }

        public TourReviewDTO TourReview { get; set; }
        public TourRatingWindowViewModel(TourDTO selectedTour, int userId) { 
            SelectedTour = selectedTour;
            UserId = userId;
            TourReview = new TourReviewDTO();
            SelectedImageIndex = -1;

            AddImageCommand = new RelayCommand(AddImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
            PreviousImageCommand = new RelayCommand(NavigatePreviousImage, CanNavigatePrevious);
            NextImageCommand = new RelayCommand(NavigateNextImage, CanNavigateNext);
        }

        private void AddImage(object obj) {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files |*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == true) {
                ImagePaths.Add(openFileDialog.FileName);
                if (SelectedImageIndex == -1) 
                    SelectedImageIndex = 0;
            }
        }

        private void UpdateImage() {
            if (_selectedImageIndex != -1)
                CurrentImagePath = ImagePaths[SelectedImageIndex];
            else {
                CurrentImagePath = null;
            }
        }

        private void DeleteImage(object obj) {
            if (SelectedImageIndex >= 0 && SelectedImageIndex < ImagePaths.Count) {
                ImagePaths.RemoveAt(SelectedImageIndex);
                if (SelectedImageIndex >= ImagePaths.Count) 
                    SelectedImageIndex = ImagePaths.Count - 1;
            }
        }

        private bool CanNavigatePrevious() {
            return SelectedImageIndex > 0;
        }

        private void NavigatePreviousImage(object obj) {
            if (CanNavigatePrevious()) 
                SelectedImageIndex--;
        }

        private bool CanNavigateNext() {
            return SelectedImageIndex < ImagePaths.Count - 1;
        }

        private void NavigateNextImage(object obj) {
            if (CanNavigateNext()) 
                SelectedImageIndex++;
        }

        private void SetImages() {
            foreach (var imagePath in ImagePaths) 
                TourReview.Pictures.Add(imagePath);
        }

        public void SendReview() {
            SetImages();
            _tourReviewService.SendReview(SelectedTour, UserId, TourReview);
        }
    }
}
