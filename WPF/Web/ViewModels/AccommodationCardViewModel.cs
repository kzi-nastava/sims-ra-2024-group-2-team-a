using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Web.ViewModels {
    public class AccommodationCardViewModel {

        public AccommodationDTO Accommodation { get; set; }
        public bool BySuperOwner { get; set; }

        private readonly OwnerService _ownerService = new OwnerService();

        public AccommodationCardViewModel(AccommodationDTO accommodation) {
            Accommodation = accommodation;

            BySuperOwner = _ownerService.GetByUserId(Accommodation.OwnerId).IsSuper;
        }
    }
}
