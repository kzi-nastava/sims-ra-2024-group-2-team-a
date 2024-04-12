﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace BookingApp.Repository {
    public class TourReservationRepository : Repository<TourReservation> {

        public List<TourReservation> GetByTourId(int tourId) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.TourId == tourId);
        }
        public TourReservation GetByTourAndTourist(int tourId, int touristId) {
            _items = _serializer.FromCSV();
            return _items.Find(x => x.TourId == tourId && x.TouristId == touristId);
        }
        public List<TourReservation> DeleteByTourId(int id) {
            List<TourReservation> reservations = GetByTourId(id);
            if (reservations == null)
                return new List<TourReservation>();
            if(DeleteMultiple(reservations))
                return reservations;
            return null;
        }
        public bool DeleteMultiple(List<TourReservation> reservations) {
            _items = _serializer.FromCSV();
            if (_items.RemoveAll(x => reservations.Select(y => y.Id).Contains(x.Id)) != reservations.Count)
                    return false;
            _serializer.ToCSV(_items);
            return true;
        }
    }
}