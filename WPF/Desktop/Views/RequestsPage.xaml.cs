using BookingApp.WPF.Desktop.ViewModels;
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

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page {
        public int UserId { get; set; }
        public RequestsPage(int userId) {
            InitializeComponent();
            DataContext = new RequestsPageViewModel(userId);
            UserId = userId;
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e) {
            StatisticsWindow statisticsWindow = new StatisticsWindow(UserId);
            statisticsWindow.Show();
        }
    }
}
