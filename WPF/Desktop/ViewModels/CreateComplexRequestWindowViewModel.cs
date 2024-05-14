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
        public CreateComplexRequestWindowViewModel(int userId) {
            UserId = userId;
            Func<bool> alwaysTrue = () => true;
            CreateComplexRequestCommand = new RelayCommand(CreateRequest, alwaysTrue);
            RemoveSimpleTourCommand = new RelayCommand(RemoveSimpleTour, alwaysTrue);
        }

        public void CreateRequest(object parameter) {
            _complexTourRequestService.CreateRequest(UserId, SimpleTourRequests.ToList());
        }

        public void RemoveSimpleTour(object parameter) {
            SimpleTourRequests.Remove((TourRequestDTO)parameter);
        }
    }
}
