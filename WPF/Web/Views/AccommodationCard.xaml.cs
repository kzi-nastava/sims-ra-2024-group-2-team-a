using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

            if (DataContext is AccommodationCardViewModel)
                return;

            AccommodationDTO accommodation = (AccommodationDTO)DataContext;
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
