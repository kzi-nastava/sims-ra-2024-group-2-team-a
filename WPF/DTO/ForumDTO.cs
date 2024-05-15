using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO
{
    public class ForumDTO : INotifyPropertyChanged {
        public ForumDTO() { }
        public ForumDTO(Forum forum) {
            Id = forum.Id;
            Title = forum.Title;
            LocationId = forum.LocationId;
            CreatorId = forum.CreatorId;
            GuestCommentNum = forum.GuestCommentNum;
            OwnerCommentNum = forum.OwnerCommentNum;
            IsUsefull = forum.IsUsefull;
            IsClosed = forum.IsClosed;
            CommentNum = "Comments: " + (GuestCommentNum + OwnerCommentNum).ToString();
        }
        public int Id { get; set; } = 0;

        private string _title;
        public string Title {
            get {
                return _title;
            }
            set {
                if (value != _title) {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId { get;set;}
        public LocationDTO Location { get; set; }
        public int CreatorId { get; set; }
        public String Username { get; set; }

        private int _guestCommentNum;
        public int GuestCommentNum {
            get {
                return _guestCommentNum;
            }
            set {
                if (value != _guestCommentNum) {
                    _guestCommentNum = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ownerCommentNum;
        public int OwnerCommentNum {
            get {
                return _ownerCommentNum;
            }
            set {
                if (value != _ownerCommentNum) {
                    _ownerCommentNum = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isUsefull;
        public bool IsUsefull {
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

        private bool _isClosed;
        public bool IsClosed {
            get {
                return _isClosed;
            }
            set {
                if (value != _isClosed) {
                    _isClosed = value;
                    OnPropertyChanged();
                }
            }
        }
        public String CommentNum { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
