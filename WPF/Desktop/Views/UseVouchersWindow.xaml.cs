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

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.5;
            this.Height = screenHeight * 0.5;

            viewModel = new UseVouchersWindowViewModel(parentViewModel);
            DataContext = viewModel;
        }

        private void UseVoucherButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
