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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristVouchers.xaml
    /// </summary>
    public partial class TouristVouchersPage : Page {
        private readonly VoucherService _voucherService;
        public int UserId { get; set; }
        public ObservableCollection<VoucherDTO> VouchersOnDisplay { get; set; }
        public TouristVouchersPage(int userId) {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _voucherService = new VoucherService();
            VouchersOnDisplay = new ObservableCollection<VoucherDTO>();
            LoadVouchers();
        }

        public void LoadVouchers() {
            VouchersOnDisplay.Clear();
            foreach(Voucher voucher in _voucherService.GetByTouristId(UserId)) {
                VouchersOnDisplay.Add(new VoucherDTO(voucher));
            }
        }
    }
}
