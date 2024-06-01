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
    /// Interaction logic for CommentCard.xaml
    /// </summary>
    public partial class CommentCard : UserControl {

        public CommentCardViewModel ViewModel { get; set; }

        public CommentCard() {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e) {
            if(ViewModel != null)
                return;

            ViewModel = DataContext as CommentCardViewModel;
            GuestMainWindow window = App.GuestMainWindowReference;

            if (window.GuestId == ViewModel.Comment.CreatorId) {
                this.HorizontalContentAlignment = HorizontalAlignment.Right;
                userImage.Visibility = Visibility.Collapsed;
            }
        }
    }
}
