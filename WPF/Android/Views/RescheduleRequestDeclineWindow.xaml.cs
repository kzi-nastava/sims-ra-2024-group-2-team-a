using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using System.Windows;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for RescheduleRequestDeclineWindow.xaml
    /// </summary>
    public partial class RescheduleRequestDeclineWindow : Window {
        
        RescheduleDeclineViewmodel RescheduleDeclineViewmodel { get; set; }
        public RescheduleRequestDeclineWindow(RescheduleRequestDTO rescheduleRequestDTO) {
            InitializeComponent();
            RescheduleDeclineViewmodel = new RescheduleDeclineViewmodel(rescheduleRequestDTO);
            DataContext = RescheduleDeclineViewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            RescheduleDeclineViewmodel.DeclineRequest();
            this.Close();
        }
    }
}
