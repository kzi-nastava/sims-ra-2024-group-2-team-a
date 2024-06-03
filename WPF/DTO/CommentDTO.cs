using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO {
    public class CommentDTO: INotifyPropertyChanged {
        public CommentDTO() { }

        public CommentDTO(Comment comment) {
            Id = comment.Id;
            Text = comment.Text;
            ForumId = comment.ForumId;
            CreatorId = comment.UserId;
            ReportsNum = comment.ReportsNum;
            CreationDate = comment.CreationTime;
            CreationDateString = $"{CreationDate.Date.ToString("dd-MM-yyyy")} {CreationDate.Hour}:{CreationDate.Minute}";
        }
        public int Id { get; set; } = 0;

        private string _text;
        public string Text {
            get {
                return _text;
            }
            set {
                if (value != _text) {
                    TextCount = value.Count();

                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _textCount;
        public int TextCount {
            get {
                return _textCount;
            }
            set {
                if (value != _textCount) {
                    _textCount = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ForumId { get; set; }
        public int CreatorId { get; set; }
        public String Username { get; set; }

        private int _reportsNum;
        public int ReportsNum {
            get {
                return _reportsNum;
            }
            set {
                if (value != _reportsNum) {
                    _reportsNum = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isReportable;
        public bool IsReportable {
            get {
                return _isReportable;
            }
            set {
                if (value != _isReportable) {
                    _isReportable = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime CreationDate { get; set; }
        public string CreationDateString { get; set; }

        public Comment ToComment() {
            return new Comment(CreationDate, Text, CreatorId, ForumId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
