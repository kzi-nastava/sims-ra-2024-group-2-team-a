using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class VoucherService {
        private readonly VoucherRepository _voucherRepository;

        public VoucherService() {
            _voucherRepository = new VoucherRepository();
        }

        public List<Voucher> GetByTouristId(int userId) {
            List<Voucher> allVouchers = _voucherRepository.GetAll();

            return allVouchers.Where(v => v.TouristId == userId).ToList();
        }

        public List<Voucher> GetAvailableVouchers(int userId) {
            return GetByTouristId(userId).Where(v => v.Used == false && v.ExpireDate > DateTime.Now).ToList();
        }

        public void RemoveVoucher(VoucherDTO selectedVoucher) {
            Voucher voucher = _voucherRepository.GetById(selectedVoucher.Id);
            voucher.Used = false;
        }
    }
}
