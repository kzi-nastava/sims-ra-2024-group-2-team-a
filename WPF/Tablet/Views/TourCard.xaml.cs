using BookingApp.DTO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views
{
    /// <summary>
    /// Interaction logic for TourCard.xaml
    /// </summary>
    public partial class TourCard : UserControl
    {
        public TourCard()
        {
            InitializeComponent();
        }

        private void TourCardClick(object sender, MouseButtonEventArgs e) {
            Frame mainFrame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            Frame menuBarFrame = (Frame)Window.GetWindow(this).FindName("menuBarFrame");
            TourDTO tourDTO = (TourDTO)DataContext;
            mainFrame.Content = new TourPage(tourDTO, mainFrame, menuBarFrame, tourDTO.GuideId);
        }
    }
}
