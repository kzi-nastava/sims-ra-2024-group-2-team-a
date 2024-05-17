using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels {
    public class ForumCommentsViewmodel {
        public ForumDTO ForumDTO { get; set; }
        public ForumCommentsViewmodel(ForumDTO forumDTO) {
            ForumDTO = forumDTO;
        }
    }
}
