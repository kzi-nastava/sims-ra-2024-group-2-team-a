using BookingApp.View.TabletView;
using BookingApp.WPF.DTO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for LiveTourCard.xaml
    /// </summary>
    public partial class LiveTourCard : UserControl {
        public LiveTourCard() {
            InitializeComponent();
        }

        private void LiveTourCardClick(object sender, MouseButtonEventArgs e) {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to start tour?", "Start tour", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.No)
                return;
            Frame mainFrame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            TourDTO tourDTO = (TourDTO)DataContext;
            mainFrame.Content = new LiveTourPage(tourDTO, mainFrame, tourDTO.GuideId);
        }
    }
}
