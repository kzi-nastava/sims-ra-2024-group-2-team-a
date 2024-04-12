using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for ViewSelectedImagesWindow.xaml
    /// </summary>
    public partial class ViewSelectedImagesWindow : Window {
        public AccommodationDTO AccommodationDTO { get; set; }

        public ObservableCollection<string> ProfilePictures { get; set; }
        public ViewSelectedImagesWindow(AccommodationDTO accommodationDTO) {
            InitializeComponent();
            DataContext = this;

            ProfilePictures = new ObservableCollection<string>();   
            AccommodationDTO = accommodationDTO;
            foreach (var v in accommodationDTO.ProfilePictures) {
                ProfilePictures.Add(v);
            }
        }

        private void DeleteSelectedImage_Click(object sender, RoutedEventArgs e) {
            string imagePath = (sender as Button)?.DataContext as string;

            if (imagePath != null) {
                AccommodationDTO.ProfilePictures.Remove(imagePath);
                ProfilePictures.Remove(imagePath);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
