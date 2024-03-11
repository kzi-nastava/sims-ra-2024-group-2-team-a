using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class PassengerRepository : IRepository<User> {
        private const string FilePath = "../../../Resources/Data/users.csv";
        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public PassengerRepository() {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public List<User> GetAll() {
            throw new System.NotImplementedException();
        }

        public void Save(User item) {
            throw new System.NotImplementedException();
        }

        public bool Update(User item) {
            throw new System.NotImplementedException();
        }

        public bool Delete(User item) {
            throw new System.NotImplementedException();
        }

        public User GetById(int id) {
            throw new System.NotImplementedException();
        }
    }
}
