using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IVoucherRepository : IRepository<Voucher>
    {
        public bool AddMultiple(List<int> TouristIds, DateTime expireDate);
    }
}
