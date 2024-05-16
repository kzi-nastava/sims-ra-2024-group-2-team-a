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

        ForumCommentsViewmodel ForumCommentsViewmodel { get; set; }
        public ForumCommentsPage(ForumDTO forumDTO) {
            InitializeComponent();

            ForumCommentsViewmodel = new ForumCommentsViewmodel(forumDTO);
            DataContext = ForumCommentsViewmodel;
        }
    }
}
