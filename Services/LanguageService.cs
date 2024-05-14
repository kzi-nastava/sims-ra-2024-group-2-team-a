using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Services {
    public class LanguageService {
        private readonly ILanguageRepository _languageRepository;
        public LanguageService(ILanguageRepository languageRepository) {
            _languageRepository = languageRepository;
        }

        public List<Language> GetAll() {
            return _languageRepository.GetAll();
        }
        public Language GetById(int id) {
            return _languageRepository.GetById(id);
        }
    }
}
