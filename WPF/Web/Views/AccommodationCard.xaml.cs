using BookingApp.DTO;
using BookingApp.WPF.Web.ViewModels;
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

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for AccommodationCard.xaml
    /// </summary>
    public partial class AccommodationCard : UserControl {

        AccommodationCardViewModel ViewModel { get; set; }

        public AccommodationCard() {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e) {
            AccommodationDTO accommodation = (AccommodationDTO) DataContext;
            ViewModel = new AccommodationCardViewModel(accommodation);
            DataContext = ViewModel;

            if (ViewModel.BySuperOwner) {
                borderHeader.Background = new SolidColorBrush(Colors.PaleGoldenrod);
                borderCorner.BorderBrush = new SolidColorBrush(Colors.PaleGoldenrod);
            }
        }

        private void AccommodationCardClick(object sender, MouseButtonEventArgs e) {
            GuestMainWindow window = (GuestMainWindow) Window.GetWindow(this);
            Frame mainFrame = window.MainFrame;

            mainFrame.Navigate(new CreateReservationPage(ViewModel.Accommodation));
        }
    }
}
