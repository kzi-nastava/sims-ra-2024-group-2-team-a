using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class LanguageService {
        private readonly LanguageRepository _languageRepository = new LanguageRepository();

        public List<Language> GetAll() {
            return _languageRepository.GetAll();
        }
        public Language GetById(int id) {
            return _languageRepository.GetById(id);
        }
    }
}
