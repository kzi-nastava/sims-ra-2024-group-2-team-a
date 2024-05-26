using BookingApp.WPF.DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Tablet.Views
{
    /// <summary>
    /// Interaction logic for TourRequestSuggestionPage.xaml
    /// </summary>
    public partial class TourRequestSuggestionPage : Page
    {
        private Frame _mainFrame, _additionalFrame;
        private int _userId;
        TourRequestSuggestionViewModel ViewModel { get; set; }
        public TourRequestSuggestionPage(Frame mFrame, Frame aFrame, int userId){
            _userId = userId;
            _mainFrame = mFrame;
            _additionalFrame = aFrame;
            InitializeComponent();
            ViewModel = new TourRequestSuggestionViewModel();
            DataContext = ViewModel;
        }

        private void Loaction_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Loaction_Executed(object sender, ExecutedRoutedEventArgs e) {
            _additionalFrame.Content = null;
            _mainFrame.Content = new AddTourPage(ViewModel.MostWantedLocation(), _mainFrame, _userId);
        }

        private void Language_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Language_Executed(object sender, ExecutedRoutedEventArgs e) {
            _additionalFrame.Content = null;
            _mainFrame.Content = new AddTourPage(ViewModel.MostWantedLanguage(), _mainFrame, _userId);
        }
    }
}
