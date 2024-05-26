using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace BookingApp.Repository {
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository{
        public bool AddMultiple(List<int> TouristIds, DateTime expireDate) {
            foreach (int id in TouristIds) {
                if (Save(new Voucher(expireDate, id)) == null)
                    return false;
            }
            return true;
        }
    }
}
