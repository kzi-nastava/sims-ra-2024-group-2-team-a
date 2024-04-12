using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

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
