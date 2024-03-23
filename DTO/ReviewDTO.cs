﻿using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ReviewDTO: INotifyPropertyChanged {
        
        public ReviewDTO() { }

        public ReviewDTO(Review review) {
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
        }

        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public int OwnerId { get; set; }

        private int _guestCleannessGrade;
        public int GuestCleannessGrade {
            get {
                return _guestCleannessGrade;
            }
            set {
                if (value != _guestCleannessGrade) {
                    _guestCleannessGrade = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ruleFollowingGrade;
        public int RuleFollowingGrade {
            get {
                return _ruleFollowingGrade;
            }
            set {
                if (value != _ruleFollowingGrade) {
                    _ruleFollowingGrade = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ownerComment;
        public string OwnerComment {
            get {
                return _ownerComment;
            }
            set {
                if (value != _ownerComment) {
                    _ownerComment = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _accommodationCleanness;
        public int AccommodationCleannessGrade {
            get {
                return _accommodationCleanness;
            }
            set {
                if (value != _accommodationCleanness) {
                    _accommodationCleanness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ownerCorrectness;
        public int OwnerCorrectnessGrade {
            get {
                return _ownerCorrectness;
            }
            set {
                if (value != _ownerCorrectness) {
                    _ownerCorrectness = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _guestComment;
        public string GuestComment {
            get {
                return _guestComment;
            }
            set {
                if (value != _guestComment) {
                    _guestComment = value;
                    OnPropertyChanged();
                }
            }
        }

        public Review ToReview() {
            return new Review(ReservationId, GuestId, OwnerId, GuestCleannessGrade, RuleFollowingGrade, OwnerComment, AccommodationCleannessGrade, OwnerCorrectnessGrade, GuestComment);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
