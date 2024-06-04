using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristVouchers.xaml
    /// </summary>
    public partial class TouristVouchersPage : Page {
        
        public int UserId { get; set; }
        
        public TouristVouchersPage(int userId) {
            InitializeComponent();
            DataContext = new TouristVouchersPageViewModel(userId);
        }
    }
}
