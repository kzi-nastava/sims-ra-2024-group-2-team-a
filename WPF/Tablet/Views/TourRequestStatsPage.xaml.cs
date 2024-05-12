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
    /// Interaction logic for TourRequestStatsPage.xaml
    /// </summary>
    public partial class TourRequestStatsPage : Page
    {
        public TourRequestStatsViewModel ViewModel { get; set; }
        public TourRequestStatsPage(){
            InitializeComponent();
            ViewModel = new TourRequestStatsViewModel();
            DataContext = ViewModel;
        }

        private void Clear_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e) {
            comboBoxTimeStats.SelectedIndex = 0;
            comboBoxLanguageStats.SelectedIndex = 0;
            comboBoxLocationStats.SelectedIndex = 0;
        }

        private void Filter_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if((comboBoxLanguageStats.SelectedIndex <= 0 && comboBoxLocationStats.SelectedIndex >= 0 && comboBoxTimeStats.SelectedIndex != -1) || (comboBoxLanguageStats.SelectedIndex >= 0 && comboBoxLocationStats.SelectedIndex <= 0 && comboBoxTimeStats.SelectedIndex != -1))
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void Filter_Executed(object sender, ExecutedRoutedEventArgs e) {
            LocationDTO location = (LocationDTO)comboBoxLocationStats.SelectedItem;
            if (location == null)
                location = new LocationDTO();

            LanguageDTO language = (LanguageDTO)comboBoxLanguageStats.SelectedItem;
            if (language == null)
                language = new LanguageDTO();

            ViewModel.SetDTO(language, location, (int)comboBoxTimeStats.SelectedValue, comboBoxLanguageStats.SelectedIndex <= 0);
            ViewModel.SetStats();
        }

        private void comboBoxTimeStats_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
