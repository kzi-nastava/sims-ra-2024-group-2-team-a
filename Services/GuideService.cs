using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GuideService{
        private readonly IGuideRepository _guideRepository;

        public GuideService(IGuideRepository guideRepository) {
            _guideRepository = guideRepository;
        }

        public Guide GetById(int id) {
            return _guideRepository.GetById(id);
        }

        public bool Delete(int id) {
            return _guideRepository.Delete(GetById(id));
        }

        /*public void UpdateStatus(int id) {
            _guideRepository
        }*/
    }
}
