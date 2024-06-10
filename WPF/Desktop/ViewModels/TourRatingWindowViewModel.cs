using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
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

        public int PicturesIndex { get; set; } = 0;
        public int MaxPictureIndex => TourReview.Pictures.Count - 1;

        public ICommand AddImageCommand { get; private set; }
        public ICommand DeleteImageCommand { get; private set; }
        public ICommand PreviousImageCommand { get; private set; }
        public ICommand NextImageCommand { get; private set; }

        public ICommand ConfirmCommand { get; private set; }

        public TourReviewDTO TourReview { get; set; }
        public TourRatingWindowViewModel(TourDTO selectedTour, int userId) { 
            SelectedTour = selectedTour;
            UserId = userId;
            TourReview = new TourReviewDTO();

            TourReview.InterestGrade = 1;
            TourReview.KnowledgeGrade = 1;
            TourReview.LanguageGrade = 1;

            AddImageCommand = new RelayCommand(AddImage);
            DeleteImageCommand = new RelayCommand(DeleteImage, CanDelete);
            PreviousImageCommand = new RelayCommand(NavigatePreviousImage, CanNavigatePrevious);
            NextImageCommand = new RelayCommand(NavigateNextImage, CanNavigateNext);
            ConfirmCommand = new RelayCommand(SendReview);
        }

        private bool CanDelete() {
            return TourReview.Pictures.Count != 0;
        }

        private void AddImage(object obj) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetFullPath(@"..\..\..\Resources\Images\");
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            openFileDialog.ShowDialog();

            string basePath = Directory.GetCurrentDirectory();
            foreach (string fileName in openFileDialog.FileNames) {
                string relativePath = GetRelativePath(basePath, fileName);
                TourReview.Pictures.Add(relativePath);
            }

            if(TourReview.Pictures.Count > 0)
                CurrentImagePath = TourReview.Pictures[0];
        }

        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private void DeleteImage(object obj) {
            TourReview.Pictures.RemoveAt(PicturesIndex);
            PicturesIndex = 0;
            if (CanDelete())
                CurrentImagePath = TourReview.Pictures[0];
            else
                CurrentImagePath = "";
        }

        private bool CanNavigatePrevious() {
            return TourReview.Pictures.Count > 1 && PicturesIndex != 0;
        }

        private void NavigatePreviousImage(object obj) {
            PicturesIndex--;
            PicturesIndex = Math.Max(PicturesIndex, 0);

            CurrentImagePath = TourReview.Pictures[PicturesIndex];
        }

        private bool CanNavigateNext() {
            return PicturesIndex < MaxPictureIndex;
        }

        private void NavigateNextImage(object obj) {
            PicturesIndex++;
            PicturesIndex = Math.Min(PicturesIndex, TourReview.Pictures.Count - 1);

            CurrentImagePath = TourReview.Pictures[PicturesIndex];
        }

        public void SendReview(object parameter) {
            _tourReviewService.SendReview(SelectedTour, UserId, TourReview);
        }
    }
}
