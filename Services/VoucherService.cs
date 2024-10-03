using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class VoucherService {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository) {
            _voucherRepository = voucherRepository;
        }

        public VoucherService() {

        }

        public void Update(Voucher voucher) {
            _voucherRepository.Update(voucher);
        }

        public Voucher GetById(int id) {
            return _voucherRepository.GetById(id);
        }

        public List<Voucher> GetByTouristId(int userId) {
                return _voucherRepository.GetAll().Where(v => v.TouristId == userId && v.Used == false).ToList();
        }

        public List<Voucher> GetAvailableVouchers(int userId) {
            return GetByTouristId(userId).Where(v => v.ExpireDate > DateTime.Now).ToList();
        }

        public void RemoveVoucher(VoucherDTO selectedVoucher) {
            Voucher voucher = _voucherRepository.GetById(selectedVoucher.Id);
            voucher.Used = false;
        }
        public bool AddMultiple(List<int> TouristIds, DateTime expireDate) {
            return _voucherRepository.AddMultiple(TouristIds, expireDate);  
        }

        public void AwardVoucher(int touristId) {
            _voucherRepository.Save(new Voucher(DateTime.Now.AddMonths(6), touristId));
        }
    }
}
