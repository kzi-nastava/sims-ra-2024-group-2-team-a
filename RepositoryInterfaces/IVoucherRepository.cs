using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    public interface IVoucherRepository : IRepository<Voucher> {
        public bool AddMultiple(List<int> TouristIds, DateTime expireDate);
    }
}
