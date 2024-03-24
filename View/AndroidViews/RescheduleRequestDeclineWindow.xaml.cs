using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
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

namespace BookingApp.View {
    /// <summary>
    /// Interaction logic for RescheduleRequestDeclineWindow.xaml
    /// </summary>
    public partial class RescheduleRequestDeclineWindow : Window {

        private RescheduleRequestRepository _rescheduleRequestRepository;

        public RescheduleRequestDTO RescheduleRequestDTO { get; set; }
        public RescheduleRequestDeclineWindow(RescheduleRequestDTO rescheduleRequestDTO) {
            InitializeComponent();
            DataContext = this;

            _rescheduleRequestRepository = new RescheduleRequestRepository();
            this.RescheduleRequestDTO = rescheduleRequestDTO;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            RescheduleRequest rescheduleRequest = RescheduleRequestDTO.ToRescheduleRequest();
            rescheduleRequest.Status = RescheduleRequestStatus.Rejected;
            _rescheduleRequestRepository.Update(rescheduleRequest);
            this.Close();
        }
    }
}
