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

        /*private void PassengerNumberConfirmationButton_Click(object sender, RoutedEventArgs e) {
            wrapPanelStackPanel.Children.Clear();

            for (int i = 0; i < _passengerNumber; i++) {
                WrapPanel wrapPanel = new WrapPanel();
                wrapPanel.Orientation = Orientation.Horizontal;
                wrapPanel.Margin = new Thickness(5);
                wrapPanel.Name = "wrapPanel" + i;

                // Add 3 text boxes to each WrapPanel
                for (int j = 0; j < 3; j++) {
                    TextBox textBox = new TextBox();
                    textBox.Margin = new Thickness(5);
                    textBox.Width = 100;
                    textBox.Name = "textBox" + i;
                    wrapPanel.Children.Add(textBox);
                }

                wrapPanelStackPanel.Children.Add(wrapPanel);
            }
        }*/

        private void ConfirmReservationButton_Click(object sender, RoutedEventArgs e) {
            SameLocationToursWindow sameLocationToursWindow = new SameLocationToursWindow(SelectedTour, this);
            int reservationSuccessIndicator = _tourReservationRepository.MakeReservation(UserId, SelectedTour, Passengers.ToList());
            if (reservationSuccessIndicator > 0) {
                string messageBoxOutput = "There is not enought space for all!\nAvailable space: " + reservationSuccessIndicator.ToString();
                MessageBox.Show(messageBoxOutput, "Title of the MessageBox", MessageBoxButton.OK);
            }
            else if (reservationSuccessIndicator == -1) {
                sameLocationToursWindow.ShowDialog();
            }
            else
                this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddPassengerButton_Click(object sender, RoutedEventArgs e) {
            PassengerDTO newPassenger = new PassengerDTO(PassengerName.Text, PassengerSurname.Text, Convert.ToInt32(PassengerAge.Text));

            PassengerName.Text = null;
            PassengerSurname.Text = null;
            PassengerAge.Text = null;

            Passengers.Add(newPassenger);
        }

        private void RemovePassengerButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var passenger = (PassengerDTO)button.DataContext;

            // Remove the passenger from the collection
            Passengers.Remove(passenger);
        }
    }
}
