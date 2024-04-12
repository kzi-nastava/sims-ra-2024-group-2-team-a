using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class UseVouchersWindowViewModel {
        private readonly VoucherService _voucherService;
        public ObservableCollection<VoucherDTO> VouchersOnDisplay { get; set; }
        public VoucherDTO SelectedVoucher { get; set; }
        public TouristReservationWindowViewModel reservationViewModel { get; set; }

        public UseVouchersWindowViewModel(TouristReservationWindowViewModel reservationViewModel) {
            _voucherService = new VoucherService();
            VouchersOnDisplay = new ObservableCollection<VoucherDTO>();
            this.reservationViewModel = reservationViewModel; 
            LoadVouchers(); 
        }

        public void LoadVouchers() {
            VouchersOnDisplay.Clear();
            foreach (Voucher voucher in _voucherService.GetAvailableVouchers(reservationViewModel.UserId)) {
                VouchersOnDisplay.Add(new VoucherDTO(voucher));
            }
        }

        public void UseVoucher() {
            reservationViewModel.IsVoucherSelected = true;
            reservationViewModel.SelectedVoucher = SelectedVoucher;
        }
    }
}
