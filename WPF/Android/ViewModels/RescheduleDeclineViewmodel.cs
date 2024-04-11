using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels {
    public class RescheduleDeclineViewmodel {
        private RescheduleRequestService rescheduleRequestService = new RescheduleRequestService();

        public RescheduleRequestDTO RescheduleRequestDTO { get; set; }
        public RescheduleDeclineViewmodel(RescheduleRequestDTO rescheduleRequestDTO) {
            RescheduleRequestDTO = rescheduleRequestDTO;
        }

        public void DeclineRequest() {
            RescheduleRequest rescheduleRequest = RescheduleRequestDTO.ToRescheduleRequest();
            rescheduleRequest.Status = RescheduleRequestStatus.Rejected;
            rescheduleRequestService.Update(rescheduleRequest);
        }

    }
}
