using BookingApp.Domain.Model;
using BookingApp.WPF.Desktop.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CreateComplexRequestWindow.xaml
    /// </summary>
    public partial class CreateComplexRequestWindow : Window {
        int UserId { get; set; }
        CreateComplexRequestWindowViewModel ViewModel { get; set; }
        public CreateComplexRequestWindow(int userId) {
            InitializeComponent();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.7;
            this.Height = screenHeight * 0.7;

            UserId = userId;
            ViewModel = new CreateComplexRequestWindowViewModel(UserId);
            this.DataContext = ViewModel;
        }

        private void AddSimpleRequestButton_Click(object sender, RoutedEventArgs e) {
            CreateRequestWindow window = new CreateRequestWindow(UserId, ViewModel);          
            window.ShowDialog();
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e) {
            App.NotificationService.ShowSuccess("Complex tour request created successfully!");
            this.Close();
        }
    }
}
