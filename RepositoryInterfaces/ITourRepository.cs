using BookingApp.Model;
using BookingApp.WPF.DTO;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    public interface ITourRepository : IRepository<Tour>{
        public Tour GetActive(int userId);
        public List<Tour> GetScheduled(int userId);
        public List<Tour> GetFinished(int userId);
        public List<Tour> GetLive(int userId);
        public List<Tour> GetByYear(int year);
        public int GetAvailableSpace(TourDTO tour);

    }
}
