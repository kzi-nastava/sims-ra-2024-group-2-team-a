using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Android.ViewModels {
    public class CommentCardViewModel : INotifyPropertyChanged {

        public CommentDTO Comment { get; set; }

        private readonly UserService _userService = ServicesPool.GetService<UserService>();
        private readonly ForumService _forumService = ServicesPool.GetService<ForumService>();
        private readonly AccommodationReservationService _reservationService = ServicesPool.GetService<AccommodationReservationService>();

        public bool IsSelfComment => App.GuestMainWindowReference.GuestId == Comment.CreatorId;

        public bool HasVisitedLocation { get; set; }

        public bool IsByOwner { get; set; }

        public CommentCardViewModel(CommentDTO comment) {
            Comment = comment;
            IsByOwner = _userService.GetById(comment.CreatorId).Category == Domain.Model.UserCategory.Owner;

            int locationId = _forumService.GetById(comment.ForumId).LocationId;
            HasVisitedLocation = _reservationService.WasVisitedByGuest(comment.CreatorId, DateTime.Now, locationId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
