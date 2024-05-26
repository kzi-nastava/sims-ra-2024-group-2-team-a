using BookingApp.WPF.Desktop.ViewModels;
using System.Windows;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for UseVouchersWindow.xaml
    /// </summary>
    public partial class UseVouchersWindow : Window {
        public TourReservationWindow tourReservationWindow;
        public UseVouchersWindowViewModel viewModel { get; set; }

        public UseVouchersWindow(TouristReservationWindowViewModel parentViewModel) {
            InitializeComponent();

            viewModel = new UseVouchersWindowViewModel(parentViewModel);
            DataContext = viewModel;
        }

        private void UseVoucherButton_Click(object sender, RoutedEventArgs e) {
            viewModel.UseVoucher();
            this.Close();
        }
    }
}
