using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.Desktop.Views;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels
{
    public class RequestsPageViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ICommand CreateRequestCommand { get; set; } 
        public int UserId { get; set; }

        private string _filter;
        public string Filter {
            get {
                return _filter;
            }
            set {
                if (value != _filter) {
                    _filter = value;
                    OnPropertyChanged();
                    DisplayTourRequests();
                }
            }
        }

        public RequestsPageViewModel(int userId) {
            CreateRequestCommand = new RelayCommand(CreateRequest, CreateRequestCanExecute);
            UserId = userId;
            Filter = "All";
        }

        public void DisplayTourRequests() {
            TourRequests.Clear();
            foreach (var tourRequest in _tourRequestService.GetFiltered(UserId, Filter)) {
                TourRequests.Add(new TourRequestDTO(tourRequest));
            }
        }

        private void CreateRequest(object parameter) {
            CreateRequestWindow window = new CreateRequestWindow(UserId);
            window.ShowDialog();
        }

        private bool CreateRequestCanExecute() {
            return true;
        }
    }
}
