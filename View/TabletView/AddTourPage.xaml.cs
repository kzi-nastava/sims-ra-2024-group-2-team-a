using BookingApp.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.TabletView
{
    /// <summary>
    /// Interaction logic for AddTour.xaml
    /// </summary>
    public partial class AddTourPage : Page
    {
        private Frame mainFrame;
        public AddTourPage(Frame mainF)
        {
            InitializeComponent();
            mainFrame = mainF;
        }

        private void pickPhotosButton_Click(object sender, RoutedEventArgs e) {
            List<string> absolutePaths = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Milos\Desktop\SIMS\sims-ra-2024-group-2-team-a\Resources\Images\Tours\";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true) {
                // Get absolute paths of selected images
                foreach (string filename in openFileDialog.FileNames) {
                    absolutePaths.Add(filename);
                }

                // Convert absolute paths to relative paths
                string basePath = Directory.GetCurrentDirectory(); // Use application directory as base
                foreach (string absolutePath in absolutePaths) {
                    string relativePath = GetRelativePath(basePath, absolutePath);
                    //TourDTO.ProfilePictures.Add(relativePath);
                }
            }
        }
        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e) {

        }

        private void confirmButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
