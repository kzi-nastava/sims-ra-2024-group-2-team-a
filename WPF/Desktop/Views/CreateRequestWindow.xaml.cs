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
        public CreateRequestWindow(int userId)
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.7;
            this.Height = screenHeight * 0.7;

            this.DataContext = new CreateRequestWindowViewModel(userId);
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
