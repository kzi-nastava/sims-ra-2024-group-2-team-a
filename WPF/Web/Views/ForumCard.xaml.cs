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
        public ForumCard() {
            InitializeComponent();
        }

        private void ForumCardClick(object sender, MouseButtonEventArgs e) {
            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);
            Frame mainFrame = window.MainFrame;

            ForumsPage forumsPage = mainFrame.Content as ForumsPage;

            ForumDTO forum = DataContext as ForumDTO;

            mainFrame.Navigate(new CommentsPage(forumsPage, window.GuestId, forum.Id));
        }
    }
}
