using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.Web.ViewModels {

    public class CommentsPageViewModel : INotifyPropertyChanged {

        public ForumDTO Forum { get; set; }
        public CommentDTO NewComment { get; set; }
        public List<CommentCardViewModel> _comments { get; set; }
        public List<CommentCardViewModel> Comments {
            get { return _comments; }
            set {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }


        private readonly CommentService _commentService = ServicesPool.GetService<CommentService>();
        private readonly UserService _userService = ServicesPool.GetService<UserService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();

        public bool IsPostingEnabled => !Forum.IsClosed;

        public CommentsPageViewModel(int guestId, ForumDTO forum) {
            Forum = forum;
            Forum.Location = new LocationDTO(_locationService.GetById(forum.LocationId));
            NewComment = new CommentDTO();
            NewComment.CreatorId = guestId;
            NewComment.ForumId = forum.Id;
        }

        public void PostComment() {
            NewComment.CreationDate = DateTime.Now;
            _commentService.Save(NewComment.ToComment());
            NewComment.Text = "";
        }

        public void UpdateComments() {
            Comments = _commentService.GetByForumId(Forum.Id)
                .Select(c => new CommentCardViewModel(new CommentDTO(c)))
                .ToList();

            Comments.ForEach(vm => vm.Comment.Username = _userService.GetById(vm.Comment.CreatorId).Username);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
