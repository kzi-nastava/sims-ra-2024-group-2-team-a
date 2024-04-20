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
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using LiveCharts;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AccommodationStatisticsPage.xaml
    /// </summary>
    public partial class AccommodationStatisticsPage : Page {
        public AccommodationStatisticsPage(AccommodationDTO accDTO) {
            InitializeComponent();
            AccommodationStatisticsViewmodel accommodationStatisticsViewmodel = new AccommodationStatisticsViewmodel(accDTO);

            DataContext = accommodationStatisticsViewmodel;
        }
    }
}
