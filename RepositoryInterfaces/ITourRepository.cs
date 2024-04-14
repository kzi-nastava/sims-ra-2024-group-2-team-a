using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
