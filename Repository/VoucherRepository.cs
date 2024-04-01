using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository{
    public class VoucherRepository : Repository<Voucher> {
        public bool AddMultiple(List<int> TouristIds, DateOnly expiries) {
            foreach (int id in TouristIds) {
                if (Save(new Voucher(expiries, id)) == null)
                    return false;
            }
            return true;
        }
    }
}
