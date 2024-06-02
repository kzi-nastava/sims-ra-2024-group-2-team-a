using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO
{
    public class ComplexTourRequestDTO : INotifyPropertyChanged
    {
        public ComplexTourRequestDTO() { }

        public ComplexTourRequestDTO(ComplexTourRequest complexTourRequest, int serialNumber) {
            Id = complexTourRequest.Id;
            Status = complexTourRequest.Status.ToString();
            Title = "Complex Request " + serialNumber;
        }

        private int _id;
        public int Id {
            get {
                return _id;
            }
            set {
                if (_id != value) {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _title;
        public string Title {
            get {
                return _title;
            }
            set {
                if (_title != value) {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status;
        public string Status {
            get {
                return _status;
            }
            set {
                if (_status != value) {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TourRequestDTO> _tourRequests = new ObservableCollection<TourRequestDTO>();
        public ObservableCollection<TourRequestDTO> TourRequests {
            get {
                return _tourRequests;
            }
            set {
                if (_tourRequests != value) {
                    _tourRequests = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
