using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class CreateComplexRequestWindowViewModel {
        private readonly ComplexTourRequestService _complexTourRequestService = ServicesPool.GetService<ComplexTourRequestService>();
        public int UserId { get; set; }
        public ObservableCollection<TourRequestDTO> SimpleTourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ICommand CreateComplexRequestCommand { get; set; } 
        public ICommand RemoveSimpleTourCommand { get; set; }

        private RequestsPageViewModel _requestsPageViewModel;
        public CreateComplexRequestWindowViewModel(int userId, RequestsPageViewModel requestsPageViewModel) {
            UserId = userId;
            CreateComplexRequestCommand = new RelayCommand(CreateRequest, IsThereEnoughtSimpleRequests);
            RemoveSimpleTourCommand = new RelayCommand(RemoveSimpleTour, () => true);
            _requestsPageViewModel = requestsPageViewModel;
        }

        public void CreateRequest(object parameter) {
            _complexTourRequestService.CreateRequest(UserId, SimpleTourRequests.ToList());
            _requestsPageViewModel.DisplayComplexTourRequests();
        }

        public void RemoveSimpleTour(object parameter) {
            SimpleTourRequests.Remove((TourRequestDTO)parameter);
        }

        private bool IsThereEnoughtSimpleRequests() {
            return SimpleTourRequests.Count() >= 2;
        }
    }
}
