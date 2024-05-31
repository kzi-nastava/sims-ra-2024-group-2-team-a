using BookingApp.WPF.DTO;
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

namespace BookingApp.WPF.Tablet.Views
{
    /// <summary>
    /// Interaction logic for TourRequestCard.xaml
    /// </summary>
    public partial class TourRequestCard : UserControl
    {
        public TourRequestCard()
        {
            InitializeComponent();
        }

        private void TourRequestClick(object sender, MouseButtonEventArgs e) {
            Frame additionalFrame = (Frame)Window.GetWindow(this).FindName("additionalFrame");
            TourRequestDTO tourRequestDTO = (TourRequestDTO)DataContext;
            AcceptTourRequestWindow window;
            if (additionalFrame.Content.ToString() == "BookingApp.WPF.Tablet.Views.ComplexTourRequestPage") {
                window = new AcceptTourRequestWindow(tourRequestDTO, additionalFrame, true);
            }
            else {
                window = new AcceptTourRequestWindow(tourRequestDTO, additionalFrame, false);
            }
            window.ShowDialog();
        }
    }
}
