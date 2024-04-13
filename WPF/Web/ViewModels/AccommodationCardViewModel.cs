using BookingApp.DTO;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
