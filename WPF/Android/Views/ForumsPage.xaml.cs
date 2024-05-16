using BookingApp.Domain.Model;
using BookingApp.WPF.Android.ViewModels;
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
    /// Interaction logic for ForumsPage.xaml
    /// </summary>
    public partial class ForumsPage : Page {

        public ForumsPageViewmodel ForumsPageViewmodel { get; set; }

        private Frame mainFrame;
        public ForumsPage(User user , Frame mainFrame) {
            InitializeComponent();
            ForumsPageViewmodel = new ForumsPageViewmodel(user);
            DataContext = ForumsPageViewmodel;
            this.mainFrame = mainFrame;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void FilterButton_Click(object sender, RoutedEventArgs e) {
            FilterForumsWindow filterForumsWindow = new FilterForumsWindow(ForumsPageViewmodel);
            filterForumsWindow.ShowDialog();
        }
    }
}
