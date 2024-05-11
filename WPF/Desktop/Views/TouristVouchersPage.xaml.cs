using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows.Controls;

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
            _voucherService = new VoucherService(RepositoryInjector.GetInstance<IVoucherRepository>());
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
