using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class UseVouchersWindowViewModel {
        private readonly VoucherService _voucherService = ServicesPool.GetService<VoucherService>();
        public ObservableCollection<VoucherDTO> VouchersOnDisplay { get; set; }
        public TouristReservationWindowViewModel reservationViewModel { get; set; }
        public ICommand UseVoucherCommand { get; set; }
        public UseVouchersWindowViewModel(TouristReservationWindowViewModel reservationViewModel) {
            _voucherService = new VoucherService(RepositoryInjector.GetInstance<IVoucherRepository>());
            VouchersOnDisplay = new ObservableCollection<VoucherDTO>();
            this.reservationViewModel = reservationViewModel; 
            UseVoucherCommand = new RelayCommand(UseVoucher, () => true);
            LoadVouchers(); 
        }

        public void LoadVouchers() {
            VouchersOnDisplay.Clear();
            foreach (Voucher voucher in _voucherService.GetAvailableVouchers(reservationViewModel.UserId)) {
                VouchersOnDisplay.Add(new VoucherDTO(voucher));
            }
        }

        private void UseVoucher(object parameter) {
            VoucherDTO selectedVoucher = (VoucherDTO)parameter;
            reservationViewModel.IsVoucherSelected = true;
            reservationViewModel.SelectedVoucher = selectedVoucher;
        }
    }
}
