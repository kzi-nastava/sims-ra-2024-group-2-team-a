using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;

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
