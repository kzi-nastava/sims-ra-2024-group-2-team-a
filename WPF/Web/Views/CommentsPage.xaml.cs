using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
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

        private readonly ForumsPage _parentPage;

        public CommentsPageViewModel ViewModel { get; set; }

        public CommentsPage(ForumsPage forumsPage, int guestId, ForumDTO forum) {
            InitializeComponent();

            _parentPage = forumsPage;
            ViewModel = new CommentsPageViewModel(guestId, forum);
            DataContext = ViewModel;
            buttonPost.IsEnabled = false;

            UpdateCommentsView();
        }

        private void PostCommentClick(object sender, RoutedEventArgs e) {
            ViewModel.PostComment();
            UpdateCommentsView();
            _parentPage.Update();
        }   

        private void UpdateCommentsView() {
            ViewModel.UpdateComments();
            scrollViewer.ScrollToBottom();
        }

        private void CommentTextChanged(object sender, TextChangedEventArgs e) {
            buttonPost.IsEnabled = textBoxComment.Text.Length > 0;
        }
    }
}
