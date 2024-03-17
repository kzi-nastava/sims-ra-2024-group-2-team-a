using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.View.DesktopViews
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window, INotifyPropertyChanged {
        private int _passengerNumber = 0;
        private TourReservationRepository _tourReservationRepository;

        public ObservableCollection<PassengerDTO> Passengers { get; set; }
        public int PassengerNumber {
            get {
                return _passengerNumber;
            }
            set {
                if (value != _passengerNumber) {
                    _passengerNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public TourDTO SelectedTour { get; set; }
        public int UserId { get; set; }
        public TourReservationWindow(TourDTO selectedTour, int userId)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;
            UserId = userId;
 
            Passengers = new ObservableCollection<PassengerDTO>();
            _tourReservationRepository = new TourReservationRepository();
        }

        private void OpenSameLocationsWindow() {
            SameLocationToursWindow sameLocationToursWindow = new SameLocationToursWindow(SelectedTour, this);
            sameLocationToursWindow.ShowDialog();
        }

        private void ShowNotEnoughSpaceMessage(int availableSpace) {
            string messageBoxOutput = "There is not enought space for all!\nAvailable space: " + availableSpace.ToString();
            MessageBox.Show(messageBoxOutput, "Title of the MessageBox", MessageBoxButton.OK);
        }

        private void HandleReservationRequest(int successIndicator) {
            if (successIndicator > 0)
                ShowNotEnoughSpaceMessage(successIndicator);
            else if (successIndicator == -1)
                OpenSameLocationsWindow();
            else
                this.Close();
        }

        private void ConfirmReservationButton_Click(object sender, RoutedEventArgs e) {
            HandleReservationRequest(_tourReservationRepository.MakeReservation(this.UserId, SelectedTour, Passengers.ToList()));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddPassengerButton_Click(object sender, RoutedEventArgs e) {
            AddNewPassenger(PassengerName.Text, PassengerSurname.Text, Convert.ToInt32(PassengerAge.Text), -1);
        }

        private void AddTouristButton_Click(object sender, RoutedEventArgs e) {
            AddNewPassenger(TouristName.Text, TouristSurname.Text, Convert.ToInt32(TouristAge.Text), UserId);
            AddTouristButton.IsEnabled = false;
        }

        private void AddNewPassenger(string name, string surname, int age, int userId) {
            Passengers.Add(new PassengerDTO(name, surname, age, userId));
            ClearForm();
        }

        private void ClearForm() {
            foreach (var textBox in new[] { PassengerName, PassengerSurname, PassengerAge, TouristName, TouristSurname, TouristAge }) 
                textBox.Text = null;
        }

        private void RemovePassengerButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var passenger = (PassengerDTO)button.DataContext;

            Passengers.Remove(passenger);

            if (passenger.UserId != -1)
                AddTouristButton.IsEnabled = true;
        }
    }
}
