using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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
using System.Windows.Shapes;

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
