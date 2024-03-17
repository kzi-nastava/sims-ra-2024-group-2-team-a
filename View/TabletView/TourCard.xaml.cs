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

namespace BookingApp.View.TabletView
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

        private void TourCardClick(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to start tour?", "Start tour", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.No)
                return;
            Frame mainFrame = (Frame) Window.GetWindow(this).FindName("mainFrame");
            TourDTO tourDTO = (TourDTO)DataContext;
            mainFrame.Content = new LiveTourPage(tourDTO, mainFrame, tourDTO.GuideId);
        }
    }
}
