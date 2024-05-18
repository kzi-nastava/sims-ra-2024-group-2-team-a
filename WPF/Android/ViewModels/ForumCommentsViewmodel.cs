using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels {
    public class ForumCommentsViewmodel {
        public ForumDTO ForumDTO { get; set; }
        public CommentDTO CommentDTO { get; set; }

        private User _user;

        private CommentService _commentService = ServicesPool.GetService<CommentService>();

        public ObservableCollection<CommentDTO> CommentDTOs { get; set; }
        public ForumCommentsViewmodel(ForumDTO forumDTO, User user) {
            ForumDTO = forumDTO;
            _user = user;

            CommentDTO = new CommentDTO();
            CommentDTOs = new ObservableCollection<CommentDTO>();
            Update();
        }
        private void Update() {
            CommentDTOs.Clear();
            foreach (var comment in _commentService.GetByForumId(ForumDTO.Id)) {
                CommentDTOs.Add(new CommentDTO(comment));
            }
        }
        public void PostClick() {
            CommentDTO.CreatorId = _user.Id;
            CommentDTO.CreationDate = DateTime.Now;
            CommentDTO.ForumId = ForumDTO.Id;

            _commentService.Save(CommentDTO.ToComment());
            this.Update();
        }
    }
}
