using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {

    public class Repository<T> : IRepository<T> where T : ISerializable, IIdentifiable, new() {

        private readonly Serializer<T> _serializer = new Serializer<T>();

        private List<T> _items;

        public Repository() { }

        virtual public bool Delete(T item) {
            T foundItem = this.GetById(item.Id);

            if (foundItem == null)
                return false;

            _items.Remove(foundItem);
            _serializer.ToCSV(_items);
            return true;
        }

        virtual public List<T> GetAll() {
            return _serializer.FromCSV();
        }

        virtual public T GetById(int id) {
            _items = _serializer.FromCSV();
            T item = _items.Find(x => x.Id == id);
            return item;
        }

        virtual public T Save(T item) {
            item.Id = NextId();
            _items = _serializer.FromCSV();
            _serializer.ToCSV(_items);
            return item;
        }

        virtual public bool Update(T item) {
            T foundItem = this.GetById(item.Id);

            if (foundItem == null)
                return false;

            int id = _items.IndexOf(foundItem);
            _items.Remove(foundItem);
            _items.Insert(id, foundItem);

            return true;
        }

        virtual public int NextId() {
            _items = _serializer.FromCSV();
            if (_items.Count < 1)
                return 1;

            return _items.Max(x => x.Id) + 1;
        }
    }
}
