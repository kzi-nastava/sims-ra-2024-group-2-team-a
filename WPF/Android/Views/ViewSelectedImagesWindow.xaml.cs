using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
