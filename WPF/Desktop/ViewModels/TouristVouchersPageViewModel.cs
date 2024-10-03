using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristVouchersPageViewModel : INotifyPropertyChanged {
        private readonly VoucherService _voucherService = new VoucherService(RepositoryInjector.GetInstance<IVoucherRepository>());
        public ObservableCollection<VoucherDTO> VouchersOnDisplay { get; set; } = new ObservableCollection<VoucherDTO>();
        public int UserId { get; set; }
        public TouristVouchersPageViewModel(int userId) {
            UserId = userId;
            LoadVouchers();
        }

        public void LoadVouchers() {
            VouchersOnDisplay.Clear();
            foreach (Voucher voucher in _voucherService.GetByTouristId(UserId)) {
                VouchersOnDisplay.Add(new VoucherDTO(voucher));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
