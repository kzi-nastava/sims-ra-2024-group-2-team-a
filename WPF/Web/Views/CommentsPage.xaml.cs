using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for CommentsPage.xaml
    /// </summary>
    public partial class CommentsPage : Page {

        private readonly CommentService _commentService = ServicesPool.GetService<CommentService>();
        private readonly ForumService _forumService = ServicesPool.GetService<ForumService>();
        private readonly UserService _userService = ServicesPool.GetService<UserService>();
        private readonly ForumsPage _parentPage;

        private int _guestId;
        private int _forumId;

        public List<CommentCardViewModel> _comments { get; set; }

        public CommentsPage(ForumsPage forumsPage, int guestId, int forumId) {
            InitializeComponent();

            _guestId = guestId;
            _forumId = forumId;
            _parentPage = forumsPage;
            buttonPost.IsEnabled = false;
            Update();
        }

        private void PostCommentClick(object sender, RoutedEventArgs e) {
            Comment newComment = new Comment(DateTime.Now, textBoxComment.Text, _guestId, _forumId);
            _commentService.Save(newComment);
            
            Update();
            _parentPage.Update();
        }   

        private void Update() {
            _comments = _commentService.GetByForumId(_forumId)
                .Select(c => new CommentCardViewModel(new CommentDTO(c)))
                .ToList();

            _comments.ForEach(vm => vm.Comment.Username = _userService.GetById(vm.Comment.CreatorId).Username);

            itemsControlComments.ItemsSource = _comments;
            scrollViewer.ScrollToBottom();
        }

        private void CommentTextChanged(object sender, TextChangedEventArgs e) {
            buttonPost.IsEnabled = textBoxComment.Text.Length > 0;
        }
    }
}
