using BookingApp.Services;
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
    /// Interaction logic for ForumCard.xaml
    /// </summary>
    public partial class ForumCard : UserControl {

        private readonly ForumService _forumService = ServicesPool.GetService<ForumService>();

        public ForumCard() {
            InitializeComponent();
        }

        private void ForumCardClick(object sender, MouseButtonEventArgs e) {
            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);
            Frame mainFrame = window.MainFrame;

            ForumsPage forumsPage = mainFrame.Content as ForumsPage;

            ForumDTO forum = DataContext as ForumDTO;

            mainFrame.Navigate(new CommentsPage(forumsPage, window.GuestId, forum));
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e) {
            ForumDTO forumDTO = DataContext as ForumDTO;
            
            if(forumDTO == null)
                return;

            if(forumDTO.IsClosed)
                mainBorder.Background = new SolidColorBrush(Colors.LightGray);
        
            if(forumDTO.CreatorId == App.GuestMainWindowReference.GuestId && !forumDTO.IsClosed) {
                closeForumButton.Visibility = Visibility.Visible;
            }
            else {
                 closeForumButton.Visibility = Visibility.Hidden;
            }

            if(forumDTO.IsUsefull) {
                usefulIndicator.Visibility = Visibility.Visible;
            }
            else {
                usefulIndicator.Visibility = Visibility.Hidden;
            }
        }

        private void CloseForumClick(object sender, RoutedEventArgs e) {
            ForumDTO forumDTO = DataContext as ForumDTO;
            _forumService.Close(forumDTO.Id);

            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);
            Frame mainFrame = window.MainFrame;
            ForumsPage forumsPage = mainFrame.Content as ForumsPage;

            forumsPage.Update();
        }
    }
}
