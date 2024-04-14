using BookingApp.WPF.Desktop.ViewModels;
using System.Windows.Controls;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristNotificationsPage.xaml
    /// </summary>
    public partial class TouristNotificationsPage : Page {
        private readonly TouristNotificationsPageViewModel viewModel;
        
        public TouristNotificationsPage(int userId) {
            InitializeComponent();
            viewModel = new TouristNotificationsPageViewModel(userId);
            DataContext = viewModel;
        }


    }
}
