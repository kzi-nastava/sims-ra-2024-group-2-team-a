using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using System.Windows;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for ViewGuestGradeWindow.xaml
    /// </summary>
    public partial class ViewGuestGradeWindow : Window {
        public ViewGuestGradeWindow(AccommodationReservationDTO selectedReservationDTO) {
            InitializeComponent();
            ViewGuestGradeViewmodel viewGuestGradeViewmodel = new ViewGuestGradeViewmodel(selectedReservationDTO);
            DataContext = viewGuestGradeViewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
