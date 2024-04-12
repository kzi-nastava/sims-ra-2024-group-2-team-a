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

namespace BookingApp.WPF.Tablet.Views{
    /// <summary>
    /// Interaction logic for FinishedTourCard.xaml
    /// </summary>
    public partial class FinishedTourCard : UserControl
    {
        public FinishedTourCard()
        {
            InitializeComponent();
        }

        private void TourCardClick(object sender, MouseButtonEventArgs e) {
            Frame mainFrame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            Frame menuBarFrame = (Frame)Window.GetWindow(this).FindName("menuBarFrame");
            TourDTO tourDTO = (TourDTO)DataContext;
            mainFrame.Content = new FinishedTourPage(tourDTO, mainFrame, menuBarFrame, tourDTO.GuideId);
        }
    }
}
