using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.AndroidViews
{
    /// <summary>
    /// Interaction logic for AddAccomodationPage.xaml
    /// </summary>
    public partial class AddAccommodationPage : Page
    {
        public Frame mainFrame;

        private readonly User _user;

        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        public AccommodationDTO accommodationDTO { get; set; }
        public LocationDTO selectedLocationDTO { get; set; }

        public ObservableCollection<LocationDTO> locationDTOs { get; set; }

        public AddAccommodationPage(Frame mainFrame,User user)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            DataContext = this;

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();

            _user = user;
            accommodationDTO = new AccommodationDTO();
            accommodationDTO.OwnerId = _user.Id;
            locationDTOs = new ObservableCollection<LocationDTO>();

            foreach (var loc in _locationRepository.GetAll()) {
                locationDTOs.Add(new LocationDTO(loc));
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Your progress will be lost, are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                mainFrame.GoBack();
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonApartment.IsChecked == true)
            {
                accommodationDTO.Type = AccommodationType.apartment;
            }
            else if (radioButtonHouse.IsChecked == true)
            {
                accommodationDTO.Type = AccommodationType.house;
            }
            else if (radioButtonHut.IsChecked == true)
            {
                accommodationDTO.Type = AccommodationType.hut;
            }

            Accommodation acc = accommodationDTO.ToAccommodation();

            acc.LocationId = selectedLocationDTO.Id;
            _accommodationRepository.Save(acc);

            //mainFrame.GoBack();
            mainFrame.Content = new AccommodationPage(mainFrame,_user); 
            // da li je ovo kaskadiranje velik problem?
            //doduse dobar pristup jel ce mi se refreshovati collection
        }

        private void SelectImages_Click(object sender, RoutedEventArgs e)
        {
            List<string> absolutePaths = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Strahinja\Desktop\ProjekatSims\sims-ra-2024-group-2-team-a\Resources\Images\Accomodations\";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Get absolute paths of selected images
                foreach (string filename in openFileDialog.FileNames)
                {
                    absolutePaths.Add(filename);
                }

                // Convert absolute paths to relative paths
                string basePath = Directory.GetCurrentDirectory(); // Use application directory as base
                foreach (string absolutePath in absolutePaths)
                {
                    string relativePath = GetRelativePath(basePath, absolutePath);
                    accommodationDTO.ProfilePictures.Add(relativePath);
                }
            }
        }
        private string GetRelativePath(string basePath, string fullPath)
        {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }
    }
}
