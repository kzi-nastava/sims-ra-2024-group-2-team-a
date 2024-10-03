using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class ForumService {
        
        private readonly IForumRepository _forumRepository;

        private AccommodationService _accommodationService;

        private NotificationService _notificationService;

        public ForumService(IForumRepository forumRepository) {
            _forumRepository = forumRepository;
        }

        public void InjectServices(AccommodationService accommodationService, NotificationService notificationService) {
            _accommodationService = accommodationService;
            _notificationService = notificationService;
        }

        public List<Forum> GetAll() {
            return _forumRepository.GetAll();
        }

        public Forum Save(Forum forum) {
            _notificationService.CreateNewForumNotification(forum.LocationId);
            return _forumRepository.Save(forum);
        }

        public void Close(int id) {
            Forum f = this.GetById(id);
            f.Close();
            _forumRepository.Update(f);
        }

        public Forum GetById(int id) {
            return _forumRepository.GetById(id);
        }

        public List<Forum> GetByLocationId(int locationId) {
            return this.GetAll().Where(x => x.LocationId == locationId).ToList();
        }

        public void Update(Forum forum) {
            _forumRepository.Update(forum);
        }

        public List<Forum> GetAllByOwnerId(int ownerId) {
            List<Forum> forums = new List<Forum>();
            List<int> locationIds = new List<int>();
            foreach (var acc in _accommodationService.GetByOwnerId(ownerId)) {
                if (!locationIds.Contains(acc.LocationId)) {
                    locationIds.Add(acc.LocationId);
                }
            }

            foreach (var locationId in locationIds) {
                forums.AddRange(this.GetByLocationId(locationId));
            }

            return forums.OrderByDescending(x => (x.OwnerCommentNum + x.GuestCommentNum)).ToList();  
        }

        public List<Forum> GetFilteredForums(int locationId, int creatorId, bool isUsefull, int ownerId) {
            List<Forum> filteredForums = new List<Forum> ();

            foreach (var forum in this.GetAllByOwnerId(ownerId)) {
                if (CheckLocationId(locationId,forum) &&
                    CheckCreatorId(creatorId,forum) &&
                    CheckUsefullness(isUsefull,forum)) {
                    filteredForums.Add(forum);
                }
            }

            return filteredForums;
        }

        private bool CheckLocationId(int locationId, Forum forum) {
            if (locationId == -1 || forum.LocationId == locationId) {
                return true;
            }

            return false;
        }

        private bool CheckCreatorId(int creatorId, Forum forum) {
            if (creatorId == -1 || forum.CreatorId == creatorId) {
                return true;
            }

            return false;
        }

        private bool CheckUsefullness(bool isUsefull, Forum forum) {
            if (!isUsefull || forum.IsUsefull) {
                return true;
            }
            return false;
        }

        public void IncreaseOwnerComment(int forumId) {
            Forum forum = this.GetById(forumId);

            forum.OwnerCommentNum++;
            forum.UpgradeToUsefull();

            this.Update(forum);
        }
    
        public int GetNumCommentsByLocationId(int locationId) {
            int numComments = 0;
            var forums = this.GetByLocationId(locationId);
            forums.ForEach(f => numComments += f.OwnerCommentNum + f.GuestCommentNum);

            return numComments;
        }
    }
}
