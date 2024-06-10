using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
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
    /// Interaction logic for TourRequestInformationWindow.xaml
    /// </summary>
    public partial class TourRequestInformationWindow : Window {
        public TourRequestInformationWindow(TourRequestDTO selectedRequest, object clazz) {
            InitializeComponent();

            SetWindowSize(clazz);

            DataContext = new TourRequestInformationWindowViewModel(selectedRequest);
        }

        private void SetWindowSize(object clazz) {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            switch (clazz) { 
                case RequestsPageViewModel requestsPageViewModel:
                    this.Width = screenWidth * 0.7;
                    this.Height = screenHeight * 0.7;
                    break;
                case ComplexInformationWindowViewModel complexInformationWindowViewModel:
                    this.Width = screenWidth * 0.6;
                    this.Height = screenHeight * 0.6;
                    break;
            }
        }
    }
}
