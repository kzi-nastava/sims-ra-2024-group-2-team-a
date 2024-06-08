using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
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

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for ComplexInformationWindow.xaml
    /// </summary>
    public partial class ComplexInformationWindow : Window {
        
        public ComplexInformationWindow(ComplexTourRequestDTO selectedRequest) {
            InitializeComponent();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.7;
            this.Height = screenHeight * 0.7;

            DataContext = new ComplexInformationWindowViewModel(selectedRequest);
        }
    }
}
