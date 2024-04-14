using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;

namespace BookingApp.Repository {

    public class TourRepository : Repository<Tour>, ITourRepository {
        public Tour GetActive(int userId) {
            _items = _serializer.FromCSV();
            return _items.Find(x => x.GuideId == userId && x.State.Equals(TourState.Active));
        }
        public List<Tour> GetScheduled(int userId) {
            DateTime today = DateTime.Today;
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.Beggining >= today && x.State.Equals(TourState.Scheduled));
        }
        public List<Tour> GetFinished(int userId) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.State.Equals(TourState.Finished));
        }

        public List<Tour> GetLive(int userId) {
            DateTime today = DateTime.Today;
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.Beggining.Date == today.Date && x.State.Equals(TourState.Scheduled));
        }

        public List<Tour> GetByYear(int year) {
            _items = _serializer.FromCSV();
            if (year == -1) 
                return _items.FindAll(x => x.State == TourState.Finished);
            else
                return _items.FindAll(x => x.Beggining.Year == year && x.State == TourState.Finished);
           
        }

        public int GetAvailableSpace(TourDTO tour) {
            return tour.MaxTouristNumber - tour.CurrentTouristNumber;
        }
    }
}
