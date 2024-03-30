using BookingApp.DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.WebViews
{
    /// <summary>
    /// Interaction logic for RescheduleReservationModalDialog.xaml
    /// </summary>
    public partial class RescheduleReservationModalDialog : UserControl
    {
        private readonly AccommodationReservationDTO _reservation;

        public RescheduleReservationModalDialog()
        {
            InitializeComponent();
        }

    }
}
