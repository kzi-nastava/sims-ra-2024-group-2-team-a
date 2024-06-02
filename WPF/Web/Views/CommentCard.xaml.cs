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

            if (ViewModel.IsSelfComment) {
                this.HorizontalContentAlignment = HorizontalAlignment.Right;
                userImage.Visibility = Visibility.Collapsed;
            }

            if(ViewModel.Comment.ReportsNum == 0) {
                reportIndicator.Visibility = Visibility.Collapsed;
            }
            else {
                commentBorder.Background = new SolidColorBrush(Colors.LightGray);
            }

            if(!ViewModel.IsSelfComment && 
                !ViewModel.IsByOwner &&
                ViewModel.HasVisitedLocation) {
                visitedIndicator.Visibility = Visibility.Visible;
            }
        }
    }
}
