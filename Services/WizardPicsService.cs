using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class WizardPicsService {
        private readonly IWizardPicsRepository _wizardPicsRepository;
        public WizardPicsService(IWizardPicsRepository wizardRepository) {
            _wizardPicsRepository = wizardRepository;
        }

        public WizardPics GetById(int id) {
            return _wizardPicsRepository.GetById(id);
        }

        public List<string> GetImages(int id) {
            return GetById(id).Pictures;
        }
    }
}
