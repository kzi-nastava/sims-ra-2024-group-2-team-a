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
        private readonly ComplexTourRequestService _complexTourRequestService = ServicesPool.GetService<ComplexTourRequestService>();
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ObservableCollection<ComplexTourRequestDTO> ComplexTourRequests { get; set; } = new ObservableCollection<ComplexTourRequestDTO>();
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
            UserId = userId;
            Filter = "All";
            DisplayComplexTourRequests();
        }

        private void DisplayTourRequests() {
            TourRequests.Clear();
            foreach (var tourRequest in _tourRequestService.GetFiltered(UserId, Filter)) {
                TourRequests.Add(new TourRequestDTO(tourRequest));
            }
        }

        private void DisplayComplexTourRequests() {
            ComplexTourRequests.Clear();
            int complexCounter = 0;
            foreach(var complexRequest in _complexTourRequestService.GetByTouristId(UserId)) {
                ComplexTourRequestDTO complexTourRequestDTO = new ComplexTourRequestDTO(complexRequest, ++complexCounter);
                int simpleCounter = 0;
                foreach (var simpleRequest in _tourRequestService.GetForComplexRequest(complexRequest.Id))
                    complexTourRequestDTO.TourRequests.Add(new TourRequestDTO(simpleRequest, ++simpleCounter, complexRequest.Status));
                ComplexTourRequests.Add(complexTourRequestDTO);
            }
        }
    }
}
