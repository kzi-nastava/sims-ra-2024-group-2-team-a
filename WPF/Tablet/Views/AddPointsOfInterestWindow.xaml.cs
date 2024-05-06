using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for AddPointsOfInterestWindow.xaml
    /// </summary>
    public partial class AddPointsOfInterestWindow : Window {
        public PointOfInterestDTO pointOfInterestDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public AddPointsOfInterestWindow(ObservableCollection<PointOfInterestDTO> pDTOs) {
            pointOfInterestDTO = new PointOfInterestDTO();
            InitializeComponent();
            DataContext = this;

            pointOfInterestDTOs = pDTOs;
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e) {
            this.Close();
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (pointOfInterestDTO.Name != "" && pointOfInterestDTO.Description != "")
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void Add_Executed(object sender, ExecutedRoutedEventArgs e) {
            pointOfInterestDTO.IsChecked = false;
            pointOfInterestDTOs.Add(pointOfInterestDTO);
            this.Close();
        }
    }
}
