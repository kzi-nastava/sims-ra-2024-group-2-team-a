using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using Syncfusion.Windows.Controls.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    /// Interaction logic for TourRequestsMainPage.xaml
    /// </summary>
    public partial class TourRequestsMainPage : Page
    {
        private Frame _additionalFrame { get; set; }
        private int _userId;
        public TourRequestsMainPage(Frame aFrame, int userId){
            InitializeComponent();
            _additionalFrame = aFrame;
            _userId = userId;
        }

       
        private void menuTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedTab = (sender as TabControl)?.SelectedItem as TabItem;
            switch (selectedTab.Header.ToString()) {
                case "Regular Tour Requests":
                    _additionalFrame.Content = new TourRequestsPage(_additionalFrame, _userId); 
                    break;

                case "Complex Tour Requests":
                    break;

                case "Stats for Requests":
                    _additionalFrame.Content = new TourRequestStatsPage();
                    break;

                case "Suggestions":
                    break;

                default: break;
            }
        }
    }
}
