using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {

    public interface IRepository<T> {
        List<T> GetAll();
        T Save(T item);
        bool Update(T item);
        bool Delete(T item);
        T GetById(int id);
    }
}
