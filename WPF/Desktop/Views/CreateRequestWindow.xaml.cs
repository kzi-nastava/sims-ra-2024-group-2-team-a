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

namespace BookingApp.WPF.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CreateRequestWindow.xaml
    /// </summary>
    public partial class CreateRequestWindow : Window
    {
        private CreateComplexRequestWindowViewModel _complexViewModel;
        public CreateRequestWindow(int userId, CreateComplexRequestWindowViewModel? complexViewModel)
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.7;
            this.Height = screenHeight * 0.7;

            _complexViewModel = complexViewModel;
            this.DataContext = new CreateRequestWindowViewModel(userId, complexViewModel);
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e) {
            if (_complexViewModel != null)
                App.NotificationService.ShowSuccess("Simple tour request added successfully!");
            else
                App.NotificationService.ShowSuccess("Tour request created successfully!");
            this.Close();
        }
    }
}
