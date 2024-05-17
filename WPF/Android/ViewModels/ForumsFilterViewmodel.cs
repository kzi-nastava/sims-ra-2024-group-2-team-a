using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels {
    public class ForumsFilterViewmodel: INotifyPropertyChanged {

        public ObservableCollection<LocationDTO> LocationDTOs { get; set; }
        public ObservableCollection<User> Users { get; set; }

        private bool _isUsefull;
        public bool ShowUsefull {
            get {
                return _isUsefull;
            }
            set {
                if (value != _isUsefull) {
                    _isUsefull = value;
                    OnPropertyChanged();
                }
            }
        }

        public LocationDTO SelectedLocation { get; set; }
        public User SelectedUser { get; set; }

        private ForumsPageViewmodel _forumsPageViewmodel;

        private UserService _userService;

        private ForumService _forumService;

        private LocationService _locationService;
        public ForumsFilterViewmodel(ForumsPageViewmodel forumsPageViewmodel) {
            _forumsPageViewmodel = forumsPageViewmodel;
            ShowUsefull = false;
            LocationDTOs = new ObservableCollection<LocationDTO>();
            Users = new ObservableCollection<User>();

            _userService = ServicesPool.GetService<UserService>();
            _forumService = ServicesPool.GetService<ForumService>();
            _locationService = ServicesPool.GetService<LocationService>();
            Update();
        }

        private void Update() {
            LocationDTOs.Clear();
            Users.Clear();

            foreach(var forumDTO in _forumsPageViewmodel.ForumDTOs) {
                if (!LocationDTOs.Contains(forumDTO.Location)) {
                    LocationDTOs.Add(forumDTO.Location);
                }

                User user = _userService.GetById(forumDTO.CreatorId);
                if (!Users.Contains(user)) {
                    Users.Add(user);
                }
            }
        }
        public void ButtonClick() {
            int locationId = -1;
            int userId = -1;

            if (SelectedLocation != null) {
                locationId = SelectedLocation.Id;
            }
            if (SelectedUser != null) {
                userId = SelectedUser.Id;
            }

            _forumsPageViewmodel.ForumDTOs.Clear();

            foreach (var forum in _forumService.GetFilteredForums(locationId,userId, ShowUsefull, _forumsPageViewmodel.Owner.Id)) {
                ForumDTO forumDTO = new ForumDTO(forum);
                forumDTO.Location = new LocationDTO(_locationService.GetById(forum.LocationId));
                forumDTO.Username = new User(_userService.GetById(forum.CreatorId)).Username;

                _forumsPageViewmodel.ForumDTOs.Add(forumDTO);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
