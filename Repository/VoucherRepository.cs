﻿using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository{
    public class VoucherRepository : Repository<Voucher> {
        public bool AddMultiple(List<int> TouristIds, DateTime expireDate) {
            foreach (int id in TouristIds) {
                if (Save(new Voucher(expireDate, id)) == null)
                    return false;
            }
            return true;
        }

        public List<Voucher> GetByTouristId(int touristId) {
            List<Voucher> allVouchers = _serializer.FromCSV();

            return allVouchers.Where(v => v.TouristId == touristId).ToList();
        }
    }
}
