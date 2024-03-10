using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public interface IRepository<T> {

        List<T> GetAll();
        void Save(T item);
        bool Update(T item);
        bool Delete(T item);
        T GetById(int id);
    }
}
