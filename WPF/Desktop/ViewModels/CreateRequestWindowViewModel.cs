using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels
{
    public class CreateRequestWindowViewModel {
        public int UsertId { get; set; }
        public TourRequestDTO TourRequest { get; set; }
        public CreateRequestWindowViewModel(int userId) { 
            UsertId = userId;
        }
    }
}
