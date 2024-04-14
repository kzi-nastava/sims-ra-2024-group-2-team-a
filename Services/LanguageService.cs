using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class LanguageService {
        private readonly ILanguageRepository _languageRepository = RepositoryInjector.GetInstance<ILanguageRepository>();

        public List<Language> GetAll() {
            return _languageRepository.GetAll();
        }
        public Language GetById(int id) {
            return _languageRepository.GetById(id);
        }
    }
}
