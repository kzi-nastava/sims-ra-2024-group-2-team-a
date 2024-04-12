using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
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
    /// Interaction logic for ReviewAccommodationModalDialog.xaml
    /// </summary>
    public partial class ReviewAccommodationModalDialog : UserControl {

        private ReviewAccommodationModalDialogViewModel ViewModel { get; set; }

        private ReservationsPage _parentPage;

        public ReviewAccommodationModalDialog(ReservationsPage parentPage, AccommodationReservationDTO reservation) {
            InitializeComponent();
            _parentPage = parentPage;
            ViewModel = new ReviewAccommodationModalDialogViewModel(reservation);
            DataContext = ViewModel;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            ViewModel.GradeOwner();
            _parentPage.CloseModalDialog();
        }

        private void checkBoxRequiresRenovationUnchecked(object sender, RoutedEventArgs e) {
            textBoxRenovationComment.Text = "";
            comboBoxRenovationImportance.SelectedIndex = 0;
        }
    }
}
