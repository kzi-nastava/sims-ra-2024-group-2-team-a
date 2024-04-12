using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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
        public int UserId { get; set; }
        private readonly VoucherService _voucherService;
        public ObservableCollection<VoucherDTO> VouchersOnDisplay { get; set; }
        public VoucherDTO SelectedVoucher { get; set; }
        public TourReservationWindow tourReservationWindow;
        public UseVouchersWindow(int userId, TourReservationWindow parentWindow) {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _voucherService = new VoucherService();
            VouchersOnDisplay = new ObservableCollection<VoucherDTO>();
            LoadVouchers();
            tourReservationWindow = parentWindow;
        }

        public void LoadVouchers() {
            VouchersOnDisplay.Clear();
            foreach (Voucher voucher in _voucherService.GetAvailableVouchers(UserId)) {
                VouchersOnDisplay.Add(new VoucherDTO(voucher));
            }
        }

        private void UseVoucherButton_Click(object sender, RoutedEventArgs e) {
            tourReservationWindow.TourReservationViewModel.IsVoucherSelected = true;
            tourReservationWindow.TourReservationViewModel.SelectedVoucher = SelectedVoucher;
            this.Close();
        }
    }
}
