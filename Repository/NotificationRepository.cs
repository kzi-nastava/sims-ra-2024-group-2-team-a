using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class NotificationRepository: Repository<Notification> {
        
        public List<Notification> GetByUserId(int userId) {
            return this.GetAll().FindAll(x=> x.UserId == userId);
        }
    }
}
