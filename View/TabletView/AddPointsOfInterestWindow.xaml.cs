using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for AddPointsOfInterestWindow.xaml
    /// </summary>
    public partial class AddPointsOfInterestWindow : Window{
        public PointOfInterestDTO pointOfInterestDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs {get; set; }
        public AddPointsOfInterestWindow(ObservableCollection<PointOfInterestDTO> pDTOs) {
            InitializeComponent();
            DataContext = this;

            pointOfInterestDTOs = pDTOs;
            pointOfInterestDTO = new PointOfInterestDTO();
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            pointOfInterestDTO.IsChecked = false;
            pointOfInterestDTOs.Add(pointOfInterestDTO);
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
