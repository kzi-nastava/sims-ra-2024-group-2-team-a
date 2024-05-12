using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using Syncfusion.Windows.Controls;
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

namespace BookingApp.WPF.Tablet.Views
{
    /// <summary>
    /// Interaction logic for AcceptTourRequestWindow.xaml
    /// </summary>
    public partial class AcceptTourRequestWindow : Window
    {
        private Frame _mainFrame;
        public TourRequestViewModel ViewModel {  get; set; }
        public AcceptTourRequestWindow(TourRequestDTO trDTO, Frame mFrame)
        {
            InitializeComponent();
            ViewModel = new TourRequestViewModel(trDTO);
            _mainFrame = mFrame;
            DataContext = ViewModel;
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e) {
            this.Close();
        }

        private void Accept_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Accept_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (ViewModel.IsAvailable()) {
                ViewModel.AcceptTourRequest();
                MessageBox.Show("Tour request accepted.", "Succecfull", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                _mainFrame.Content = new TourRequestsMainPage(ViewModel.GetUserId());
            }
            else {
                MessageBox.Show("You are not Available at that time span", "UNAVAILABLE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void datePickerTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            ViewModel.SetToDate(sender.ToDateTime());
        }

        private void datePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            ViewModel.SetFromDate(sender.ToDateTime());
        }
    }
}
