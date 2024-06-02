using BookingApp.Domain.Model;
using BookingApp.WPF.DTO;
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
    /// Interaction logic for QuickBookModalDialog.xaml
    /// </summary>
    public partial class QuickBookModalDialog : UserControl {

        private readonly BookingPage _parentPage;

        public QuickBookModalDialogViewModel ViewModel { get; set; }

        public QuickBookModalDialog(BookingPage bookingPage, AccommodationDTO accommodation, AccommodationReservationFilterDTO resFilter) {
            InitializeComponent();

            GuestMainWindow window = App.GuestMainWindowReference;
            ViewModel = new QuickBookModalDialogViewModel(accommodation, window.GuestId, resFilter);
            DataContext = ViewModel;
            
            _parentPage = bookingPage;
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            AccommodationReservation selectedReservation = (AccommodationReservation)dataGridSuggestedDates.SelectedItem;
            ViewModel.SaveReservation(selectedReservation);
            _parentPage.CloseModalDialog();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }
    }
}
