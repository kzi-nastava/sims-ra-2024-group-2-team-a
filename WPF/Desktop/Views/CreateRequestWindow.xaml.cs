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
        public CreateRequestWindow(int userId, CreateComplexRequestWindowViewModel? complexViewModel, RequestsPageViewModel requestsPageViewModel)
        {
            InitializeComponent();
            _complexViewModel = complexViewModel;

            SetWindowSize(complexViewModel);

            CreateRequestWindowViewModel viewModel = new CreateRequestWindowViewModel(userId, complexViewModel, requestsPageViewModel);
            viewModel.CloseAction = new Action(this.Close);
            this.DataContext = viewModel;
        }

        private void SetWindowSize(CreateComplexRequestWindowViewModel? complexViewModel) {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            if (complexViewModel == null) {
                this.Width = screenWidth * 0.7;
                this.Height = screenHeight * 0.7;
            }
            else {
                this.Width = screenWidth * 0.6;
                this.Height = screenHeight * 0.5;
            }
            
        }
    }
}
