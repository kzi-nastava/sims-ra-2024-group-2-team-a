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
    /// Interaction logic for ForumCommentCard.xaml
    /// </summary>
    public partial class ForumCommentCard : UserControl {
        public ForumCommentCardViewmodel Viewmodel{ get; set; }
        public ForumCommentCard() {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            if (DataContext is ForumCommentCardViewmodel)
                return;

            CommentDTO comment = (CommentDTO)DataContext;
            Viewmodel = new ForumCommentCardViewmodel(comment);
            DataContext = Viewmodel;
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e) {
            Viewmodel.ReportCommentClick();
        }
    }
}
