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

namespace BookingApp.WPF.Desktop.ViewModels {
    public class ComplexInformationWindowViewModel {
        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();
        public ComplexTourRequestDTO SelectedRequest { get; set; }
        public ObservableCollection<TourRequestDTO> SimpleRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ICommand SimpleTourInformationCommand { get; set; }
        public ComplexInformationWindowViewModel(ComplexTourRequestDTO tourRequest) {
            SelectedRequest = tourRequest;
            SimpleTourInformationCommand = new RelayCommand(ShowSimpleRequestInformation, () => true);
            LoadSimpleRequests();
        }

        private void LoadSimpleRequests() {
            SimpleRequests.Clear();
            foreach (var request in _tourRequestService.GetForComplexRequest(SelectedRequest.Id)) 
                SimpleRequests.Add(new TourRequestDTO(request));
        }

        private void ShowSimpleRequestInformation(object parameter) {
            var simpleRequest = (TourRequestDTO)parameter;

            TourRequestInformationWindow window = new TourRequestInformationWindow(simpleRequest, this);
            window.ShowDialog();
        }
    }
}
