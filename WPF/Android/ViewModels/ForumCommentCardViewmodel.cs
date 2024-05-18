using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels {
    public class ForumCommentCardViewmodel: INotifyPropertyChanged {

        CommentService _commentService = ServicesPool.GetService<CommentService>();

        UserService _userService = ServicesPool.GetService<UserService>();

        ForumService _forumService = ServicesPool.GetService<ForumService>();
        public bool IsOwner { get; set; }
        public CommentDTO CommentDTO { get; set; }

        private bool _reportEnabled;
        public bool ReportButtonEnabled {
            get {
                return _reportEnabled;
            }
            set {
                if (value != _reportEnabled) {
                    _reportEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public ForumCommentCardViewmodel(CommentDTO comment) {
            CommentDTO = comment;
            CommentDTO.Username = _userService.GetById(comment.CreatorId).Username;
            Forum forum = _forumService.GetById(comment.ForumId);

            if (_userService.GetById(comment.CreatorId).Category == Domain.Model.UserCategory.Owner) {
                IsOwner = true;
                CommentDTO.IsReportable = false;
            }
            else {
                IsOwner= false;
                CommentDTO.IsReportable = _commentService.IsCommentReportable(comment.CreatorId,comment.CreationDate, forum.LocationId);
            }

            ReportButtonEnabled = true;
        }

        public void ReportCommentClick() {
            if (!CommentDTO.IsReportable) {
                return;
            }

            if (!ReportButtonEnabled) {
                return;
            }

            ReportButtonEnabled = false;

            _commentService.ReportComment(CommentDTO.Id);
            CommentDTO.ReportsNum++;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
