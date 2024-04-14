using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TourRatingWindow.xaml
    /// </summary>
    public partial class TourRatingWindow : Window
    {
        TourRatingWindowViewModel viewModel;
        public TourRatingWindow(TourDTO selectedTour, int userId)
        {
            InitializeComponent();
            viewModel = new TourRatingWindowViewModel(selectedTour, userId);
            DataContext = viewModel;
        }

        private void ConfirmationButton_Click(object sender, RoutedEventArgs e) {
            viewModel.SendReview();
            this.Close();
        }

        private void PostPhotosButton_Click(object sender, RoutedEventArgs e) {
            List<string> absolutePaths = new List<string>();
            OpenFileDialog openFileDialog = SetOpenFile();

            if (openFileDialog.ShowDialog() == true) {
                // Get absolute paths of selected images
                foreach (string filename in openFileDialog.FileNames) {
                    absolutePaths.Add(filename);
                }

                // Convert absolute paths to relative paths
                string basePath = Directory.GetCurrentDirectory(); // Use application directory as base
                foreach (string absolutePath in absolutePaths) {
                    string relativePath = GetRelativePath(basePath, absolutePath);
                    viewModel.TourReview.Pictures.Add(relativePath);
                }
            }
        }

        private string GetRelativePath(string basePath, string fullPath) {
            Uri baseUri = new Uri(basePath + System.IO.Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private OpenFileDialog SetOpenFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Milos\Desktop\SIMS\sims-ra-2024-group-2-team-a\Resources\Images\Tours\";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            return openFileDialog;
        }
    }
}
