using BookingApp.Domain.Model;
using System.Collections;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface INotificationRepository : IRepository<Notification>
    {
        public List<Notification> GetReadForUser(int userId);
        public List<Notification> GetNonReadForUser(int userId);
    }
}
