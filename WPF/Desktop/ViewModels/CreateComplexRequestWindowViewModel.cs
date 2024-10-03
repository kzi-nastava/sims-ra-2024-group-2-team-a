using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class CreateComplexRequestWindowViewModel : ValidationBase, INotifyPropertyChanged {
        private readonly ComplexTourRequestService _complexTourRequestService = ServicesPool.GetService<ComplexTourRequestService>();
        public int UserId { get; set; }
        public ObservableCollection<TourRequestDTO> SimpleTourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ICommand CreateComplexRequestCommand { get; set; } 
        public ICommand RemoveSimpleTourCommand { get; set; }
        public Action CloseAction { get; set; }

        private RequestsPageViewModel _requestsPageViewModel;
        public CreateComplexRequestWindowViewModel(int userId, RequestsPageViewModel requestsPageViewModel) {
            UserId = userId;
            CreateComplexRequestCommand = new RelayCommand(CreateRequest, () => true);
            RemoveSimpleTourCommand = new RelayCommand(RemoveSimpleTour, () => true);
            _requestsPageViewModel = requestsPageViewModel;
        }

        public void CreateRequest(object parameter) {
            Validate();

            if (!IsValid)
                return;

            _complexTourRequestService.CreateRequest(UserId, SimpleTourRequests.ToList());
            _requestsPageViewModel.DisplayComplexTourRequests();
            App.NotificationService.ShowSuccess("Complex tour request created successfully!");
            CloseAction?.Invoke();
        }

        public void RemoveSimpleTour(object parameter) {
            SimpleTourRequests.Remove((TourRequestDTO)parameter);
        }

        protected override void ValidateSelf() {
            ValidationErrors.Clear();

            if (SimpleTourRequests.Count() < 2)
                ValidationErrors["Confirmation"] = "A minimum of 2 simple requests must be added!";

            OnPropertyChanged(nameof(ValidationErrors));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
