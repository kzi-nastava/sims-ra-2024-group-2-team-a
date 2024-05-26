using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Android.ViewModels {
    public class RescheduleDeclineViewmodel {

        private AccommodationRescheduleRequestService rescheduleRequestService = ServicesPool.GetService<AccommodationRescheduleRequestService>();

        public AccommodationRescheduleRequestDTO RescheduleRequestDTO { get; set; }
        public RescheduleDeclineViewmodel(AccommodationRescheduleRequestDTO rescheduleRequestDTO) {
            RescheduleRequestDTO = rescheduleRequestDTO;
        }

        public void DeclineRequest() {
            AccommodationRescheduleRequest rescheduleRequest = RescheduleRequestDTO.ToRescheduleRequest();
            rescheduleRequest.Status = RescheduleRequestStatus.Rejected;
            rescheduleRequestService.Update(rescheduleRequest);
        }

    }
}
