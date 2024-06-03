using BookingApp.Domain.Model;
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

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for ForumCommentsPage.xaml
    /// </summary>
    public partial class ForumCommentsPage : Page {

        public ForumCommentsViewmodel ForumCommentsViewmodel { get; set; }

        private ForumsPageViewmodel _forumsPageViewmodel;
        public ForumCommentsPage(ForumDTO forumDTO, User user, ForumsPageViewmodel forumsPageViewmodel) {
            InitializeComponent();

            ForumCommentsViewmodel = new ForumCommentsViewmodel(forumDTO, user);
            DataContext = ForumCommentsViewmodel;
            _forumsPageViewmodel = forumsPageViewmodel;
            scrollViewer.ScrollToBottom();
        }

        private void PostButton_Click(object sender, RoutedEventArgs e) {
            ForumCommentsViewmodel.PostClick();
            _forumsPageViewmodel.Update();
            scrollViewer.ScrollToBottom();
        }
    }
}
