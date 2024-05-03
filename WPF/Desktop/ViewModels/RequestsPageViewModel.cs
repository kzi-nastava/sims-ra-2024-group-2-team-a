using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.Desktop.Views;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels
{
    public class RequestsPageViewModel {
        private readonly TourRequestService _tourRequestService = new TourRequestService(RepositoryInjector.GetInstance<ITourRequestRepository>());
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ICommand CreateRequestCommand { get; set; } 
        public int UserId { get; set; }

        public RequestsPageViewModel(int userId) {
            CreateRequestCommand = new RelayCommand(CreateRequest, CreateRequestCanExecute);
            UserId = userId;
            DisplayTourRequests();
        }

        public void DisplayTourRequests() {
            TourRequests.Clear();
            foreach (var tourRequest in _tourRequestService.GetByTouristId(UserId)) {
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
