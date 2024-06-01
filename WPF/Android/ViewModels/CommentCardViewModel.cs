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

        public bool IsOwner { get; set; }

        public CommentCardViewModel(CommentDTO comment) {
            Comment = comment;
            IsOwner = _userService.GetById(comment.CreatorId).Category == Domain.Model.UserCategory.Owner;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
