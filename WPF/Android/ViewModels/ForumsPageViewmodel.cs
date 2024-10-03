using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels
{
    public class ForumsPageViewmodel
    {
        private ForumService _forumService = ServicesPool.GetService<ForumService>();

        private LocationService _locationService = ServicesPool.GetService<LocationService>();

        private UserService _userService = ServicesPool.GetService<UserService>();
        public ObservableCollection<ForumDTO> ForumDTOs { get; set; }
        public ForumDTO SelectedForum { get; set; }

        public User Owner;

        public AndroidCommand ClearCommand { get; set; }
        public ForumsPageViewmodel(User user) {
            Owner = user;
            ForumDTOs = new ObservableCollection<ForumDTO> { };

            ClearCommand = new AndroidCommand(ClearExecuted, ClearCanBeExecuted);
            Update();
        }

        public void Update() {
            ForumDTOs.Clear();

            foreach (var forum in _forumService.GetAllByOwnerId(Owner.Id)) {
                ForumDTO forumDTO = new ForumDTO(forum);
                forumDTO.Location = new LocationDTO(_locationService.GetById(forum.LocationId));
                forumDTO.Username = new User(_userService.GetById(forum.CreatorId)).Username;

                ForumDTOs.Add(forumDTO);
            }
        }

        public void ResetSelectedForum() {
            SelectedForum = null;
        }
        public void ClearExecuted(object obj) {
            this.Update();
        }
        public bool ClearCanBeExecuted(object obj) {
            return true;
        }
    }
}
