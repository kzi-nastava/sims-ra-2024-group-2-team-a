using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.View.TabletView;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for LiveTourCard.xaml
    /// </summary>
    public partial class LiveTourCard : UserControl {
        private readonly TourRepository _tourRepository;
        public LiveTourCard() {
            InitializeComponent();
            _tourRepository = new TourRepository();
        }

        private void LiveTourCardClick(object sender, MouseButtonEventArgs e) {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to start tour?", "Start tour", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.No)
                return;
            Frame mainFrame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            Frame menuBarFrame = (Frame)Window.GetWindow(this).FindName("menuBarFrame");
            TourDTO tourDTO = (TourDTO)DataContext;
            tourDTO.State = Model.TourState.Active;
            _tourRepository.Update(tourDTO.ToModel());
            mainFrame.Content = new LiveTourPage(tourDTO, mainFrame, menuBarFrame, tourDTO.GuideId);
        }
    }
}
