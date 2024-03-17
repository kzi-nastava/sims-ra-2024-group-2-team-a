using BookingApp.DTO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for TourCard.xaml
    /// </summary>
    public partial class TourCard : UserControl {
        public TourCard() {
            InitializeComponent();
        }

        private void TourCardClick(object sender, MouseButtonEventArgs e) {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to start tour?", "Start tour", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.No)
                return;
            Frame mainFrame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            Frame menuBarFrame = (Frame)Window.GetWindow(this).FindName("menuBarFrame");
            TourDTO tourDTO = (TourDTO)DataContext;
            mainFrame.Content = new LiveTourPage(tourDTO, mainFrame, menuBarFrame, tourDTO.GuideId);
        }
    }
}
