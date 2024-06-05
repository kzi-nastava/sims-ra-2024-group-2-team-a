using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window {
        private ReportWindowViewModel _viewModel;
        public ReportWindow(int userId, TourDTO tour) {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.5;
            this.Height = screenHeight * 0.7;

            _viewModel = new ReportWindowViewModel(userId, tour);
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            App.NotificationService.ShowSuccess("Your report has been saved.");
            this.Close();
        }
    }
}
