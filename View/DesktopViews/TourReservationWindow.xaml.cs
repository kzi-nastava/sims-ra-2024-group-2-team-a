using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View.DesktopViews
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window, INotifyPropertyChanged {
        private int _passengerNumber = 0;
        private TourReservationRepository _tourReservationRepository;

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
            _tourReservationRepository = new TourReservationRepository();
        }

        private void PassengerNumberConfirmationButton_Click(object sender, RoutedEventArgs e) {
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
        }

        private void ConfirmReservationButton_Click(object sender, RoutedEventArgs e) {
            List<PassengerDTO> passengerList = new List<PassengerDTO>();
            for(int i = 0; i < _passengerNumber; i++) {
                WrapPanel wrapPanel = (WrapPanel)wrapPanelStackPanel.Children[i];
                PassengerDTO passengerDTO = new PassengerDTO(((TextBox)wrapPanel.Children[0]).Text, ((TextBox)wrapPanel.Children[1]).Text, Convert.ToInt32(((TextBox)wrapPanel.Children[2]).Text));
                passengerList.Add(passengerDTO);
            }
            _tourReservationRepository.MakeReservation(UserId, SelectedTour, passengerList);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
