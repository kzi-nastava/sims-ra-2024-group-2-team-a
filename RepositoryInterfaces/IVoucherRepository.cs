using BookingApp.Model;
using System;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    public interface IVoucherRepository : IRepository<Voucher> {
        public bool AddMultiple(List<int> TouristIds, DateTime expireDate);
    }
}
