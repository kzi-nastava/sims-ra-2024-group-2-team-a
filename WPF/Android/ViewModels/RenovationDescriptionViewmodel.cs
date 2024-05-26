using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.Android.ViewModels { 
    public class RenovationDescriptionViewmodel {
        public AccommodationRenovationDTO RenovationDTO { get; set; }

        private readonly AccommodationRenovationService _renovationService;
        public AndroidCommand ConfirmCommand { get; set; }

        private readonly Window Window;
        public bool Editable { get; set; }
        public RenovationDescriptionViewmodel(AccommodationRenovationDTO renovationDTO, Window window, bool editable) {
            RenovationDTO = renovationDTO;
            _renovationService = ServicesPool.GetService<AccommodationRenovationService>();
            Window = window;
            Editable = editable;
            ConfirmCommand = new AndroidCommand(Confirm_Executed,Confirm_CanExecute);
        }

        public void Confirm_Executed(object obj) {
            if(Editable)
                _renovationService.Save(RenovationDTO.ToAccommodationRenovation());
            Window.Close();
        }

        public bool Confirm_CanExecute(object obj) {
            return true;
        }

    }
}
