using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.Domain.Model;
using System.Numerics;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for ReservationReviewsPage.xaml
    /// </summary>
    public partial class ReservationReviewsPage : Page {

        public ReservationReviewsViewmodel ReservationReviewsViewmodel { get; set; }
        public ReservationReviewsPage(User user) {
            InitializeComponent();
            ReservationReviewsViewmodel = new ReservationReviewsViewmodel(user);
            
            this.DataContext = ReservationReviewsViewmodel;
        }
        private void AssignGradeButton_Click(object sender, RoutedEventArgs e) {
            if (!ReservationReviewsViewmodel.AssignGradeButton())
            {
                AndroidDialogWindow androidDialogWindow = new AndroidDialogWindow("Please select a reservation first!");
                androidDialogWindow.ShowDialog();
            }

        }
        private void ViewGradeButton_Click(object sender, RoutedEventArgs e) {
            string error = ReservationReviewsViewmodel.ViewGradeButton();
            if (error != null) {
                AndroidDialogWindow androidDialogWindow = new AndroidDialogWindow(error);
                androidDialogWindow.ShowDialog();
            }
        }
        private void RequestsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ReservationReviewsViewmodel.RequestsListSelectionChanged();
        }

        private void Accept_Click(object sender, RoutedEventArgs e) {
            if (ReservationReviewsViewmodel.SelectedRequest == null) {
                AndroidDialogWindow androidDialogWindow = new AndroidDialogWindow("Please select a request first!");
                androidDialogWindow.ShowDialog();
                return;
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e) {
            if (ReservationReviewsViewmodel.SelectedRequest == null) {
                AndroidDialogWindow androidDialogWindow = new AndroidDialogWindow("Please select a request first!");
                androidDialogWindow.ShowDialog();
                return;
            }
        }
    }
}
