using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class LanguageRepository : IRepository<Language> {
        private readonly Serializer<Language> _serializer;

        private List<Language> _languages;

        public LanguageRepository() {
            _serializer = new Serializer<Language>();
            _languages = _serializer.FromCSV();
        }

        public List<Language> GetAll() {
            return _serializer.FromCSV();
        }

        public Language Save(Language language) {
            language.Id = NextId();
            _languages = _serializer.FromCSV();
            _languages.Add(language);
            _serializer.ToCSV(_languages);
            return language;
        }

        public bool Delete(Language language) {
            _languages = _serializer.FromCSV();
            Language? found = _languages.Find(c => c.Id == language.Id);
            if (found == null) {
                return false;
            }
            _languages.Remove(found);
            _serializer.ToCSV(_languages);
            return true;
        }
        public Language GetById(int id) {
            _languages = _serializer.FromCSV();
            Language language = _languages.Find(c => c.Id == id);
            return language;
        }

        public bool Update(Language language) {
            _languages = _serializer.FromCSV();
            Language current = _languages.Find(c => c.Id == language.Id);
            int index = _languages.IndexOf(current);
            if (current == null) {
                return false;
            }
            _languages.Remove(current);
            _languages.Insert(index, language);       // keep ascending order of ids in file 
            _serializer.ToCSV(_languages);
            return true;
        }

        public int NextId() {
            _languages = _serializer.FromCSV();
            if (_languages.Count < 1) {
                return 1;
            }
            return _languages.Max(c => c.Id) + 1;
        }
    }
}
