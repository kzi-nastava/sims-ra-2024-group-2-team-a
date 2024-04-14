using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class VoucherService {
        private readonly IVoucherRepository _voucherRepository = RepositoryInjector.GetInstance<IVoucherRepository>();

        public VoucherService() {

        }

        public List<Voucher> GetByTouristId(int userId) {
            return _voucherRepository.GetAll().Where(v => v.TouristId == userId).ToList();
        }

        public List<Voucher> GetAvailableVouchers(int userId) {
            return GetByTouristId(userId).Where(v => v.Used == false && v.ExpireDate > DateTime.Now).ToList();
        }

        public void RemoveVoucher(VoucherDTO selectedVoucher) {
            Voucher voucher = _voucherRepository.GetById(selectedVoucher.Id);
            voucher.Used = false;
        }
        public bool AddMultiple(List<int> TouristIds, DateTime expireDate) {
            return _voucherRepository.AddMultiple(TouristIds, expireDate);  
        }
    }
}
