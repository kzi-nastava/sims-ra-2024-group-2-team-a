using BookingApp.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class ReviewDTO : INotifyPropertyChanged
    {

        public ReviewDTO() { }

        public ReviewDTO(Review review)
        {
            Id = review.Id;
            ReservationId = review.ReservationId;
            GuestId = review.GuestId;
            OwnerId = review.OwnerId;
            GuestCleannessGrade = review.GuestCleannessGrade;
            RuleFollowingGrade = review.RuleFollowingGrade;
            OwnerComment = review.OwnerComment;
            AccommodationCleannessGrade = review.AccommodationCleannessGrade;
            OwnerCorrectnessGrade = review.OwnerCorrectnessGrade;
            GuestComment = review.GuestComment;
            RequiresRenovation = review.RequiresRenovation;
            Importance = review.Importance;
            RenovationComment = review.RenovationComment;
            AccommodationPhotos.AddRange(review.AccommodationPhotos);
        }

        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public int OwnerId { get; set; }

        private int _guestCleannessGrade;
        public int GuestCleannessGrade
        {
            get
            {
                return _guestCleannessGrade;
            }
            set
            {
                if (value != _guestCleannessGrade)
                {
                    _guestCleannessGrade = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ruleFollowingGrade;
        public int RuleFollowingGrade
        {
            get
            {
                return _ruleFollowingGrade;
            }
            set
            {
                if (value != _ruleFollowingGrade)
                {
                    _ruleFollowingGrade = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ownerComment;
        public string OwnerComment
        {
            get
            {
                return _ownerComment;
            }
            set
            {
                if (value != _ownerComment)
                {
                    _ownerComment = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _accommodationCleanness;
        public int AccommodationCleannessGrade
        {
            get
            {
                return _accommodationCleanness;
            }
            set
            {
                if (value != _accommodationCleanness)
                {
                    _accommodationCleanness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ownerCorrectness;
        public int OwnerCorrectnessGrade
        {
            get
            {
                return _ownerCorrectness;
            }
            set
            {
                if (value != _ownerCorrectness)
                {
                    _ownerCorrectness = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _guestComment;
        public string GuestComment
        {
            get
            {
                return _guestComment;
            }
            set
            {
                if (value != _guestComment)
                {
                    _guestComment = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _requiresRenovation;
        public bool RequiresRenovation
        {
            get
            {
                return _requiresRenovation;
            }
            set
            {
                if (value != _requiresRenovation)
                {
                    _requiresRenovation = value;
                    OnPropertyChanged();
                }
            }
        }

        private ImportanceType _importance;
        public ImportanceType Importance
        {
            get
            {
                return _importance;
            }
            set
            {
                if (value != _importance)
                {
                    _importance = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _renovationComment;
        public string RenovationComment
        {
            get
            {
                return _renovationComment;
            }
            set
            {
                if (value != _renovationComment)
                {
                    _renovationComment = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> AccommodationPhotos { get; set; } = new List<string>();

        public Review ToReview()
        {
            var review = new Review(ReservationId, GuestId, OwnerId, GuestCleannessGrade, RuleFollowingGrade, OwnerComment, AccommodationCleannessGrade, OwnerCorrectnessGrade, GuestComment, RequiresRenovation, Importance, RenovationComment, AccommodationPhotos);
            review.Id = Id;
            return review;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
