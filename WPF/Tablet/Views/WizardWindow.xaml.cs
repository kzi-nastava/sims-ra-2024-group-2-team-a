using BookingApp.WPF.Tablet.ViewModels;
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

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow : Window {
        public WizardViewModel ViewModel { get; set; }
        private int _userId;
        private bool _firstTime;
        public WizardWindow(int userId, int wizardId, bool firstTime) {
            _userId = userId;
            _firstTime = firstTime;
            ViewModel = new WizardViewModel(wizardId);
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Previous_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (ViewModel.CanNavigatePrevious()) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
        }

        private void Previous_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (ViewModel.isAbleToEnd) {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to skip wizard?", "Skip Wizard", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.No)
                    return;

                if (_firstTime) {
                    GuideMainWindow guideMainWindow = new GuideMainWindow(_userId);
                    guideMainWindow.Show();
                }
                Close(); ;
            }
            else
                ViewModel.NavigatePreviousImage();
        }

        private void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (ViewModel.CanNavigateNext()) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
        }

        private void Next_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (ViewModel.isAbleToEnd) {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to finish wizard?", "Finish Wizard", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.No)
                    return;

                if(_firstTime) {
                    GuideMainWindow guideMainWindow  = new GuideMainWindow(_userId);
                    guideMainWindow.Show(); 
                }
                Close();
            }
            else
                ViewModel.NavigateNextImage();
        }
    }
}
