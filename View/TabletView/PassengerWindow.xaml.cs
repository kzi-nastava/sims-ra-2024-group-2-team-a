using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for PassengerWindow.xaml
    /// </summary>
    public partial class PassengerWindow : Window {
        private int _tourId;
        private int _pointOfInterestId;
        private readonly PassengerRepository _passengerRepository;

        public PassengerDTO passengerDTO { get; set; }
        public ObservableCollection<PassengerDTO> passengerDTOs { get; set; }
        public List<PassengerDTO> joinedPassengerDTOs { get; set; }
        public PassengerWindow(int tourID, int pointOfInterestId) {
            InitializeComponent();
            DataContext = this;
            _tourId = tourID;
            _pointOfInterestId = pointOfInterestId;
            _passengerRepository = new PassengerRepository();
            passengerDTOs = new ObservableCollection<PassengerDTO>();
            joinedPassengerDTOs = new List<PassengerDTO>();
            foreach (var passenger in _passengerRepository.GetUnJoined(tourID)) {
                passengerDTOs.Add(new PassengerDTO(passenger));
            }
        }

        private void joinPassengerTourComboBox_Click(object sender, RoutedEventArgs e) {
            var checkBox = (CheckBox)sender;
            var passenger = (PassengerDTO)checkBox.DataContext;
            if ((bool)checkBox.IsChecked)
                passenger.IsJoined = true;
            else
                passenger.IsJoined = false;

        }
        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
            this.Close();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e) {
            foreach(var passengerDTO in passengerDTOs) {
                if(passengerDTO.IsJoined)
                    passengerDTO.JoinedPointOfInterestId = _pointOfInterestId;
                _passengerRepository.Update(passengerDTO.ToModel());
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
