using BookingApp.Domain.Model;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.Tablet.Views;
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

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AccommodationGuidancePage.xaml
    /// </summary>
    public partial class AccommodationGuidancePage : Page {

        private AccommodationGuidanceViewmodel accommodationGuidanceViewmodel;

        private Frame mainFrame;

        private User _user;
        public AccommodationGuidancePage(User user, Frame mainFrame) {
            InitializeComponent();
            accommodationGuidanceViewmodel = new AccommodationGuidanceViewmodel(user.Id);
            _user = user;

            DataContext = accommodationGuidanceViewmodel;
            this.mainFrame = mainFrame;
        }

        private void FirstLocationButton_Click(object sender, RoutedEventArgs e) {
            if (accommodationGuidanceViewmodel.FirstLocation == null)
                return;

            mainFrame.NavigationService.Navigate(new AddAccommodationPage(mainFrame,_user,accommodationGuidanceViewmodel.FirstLocation));
        }

        private void SecondLocationButton_Click(object sender, RoutedEventArgs e) {
            if (accommodationGuidanceViewmodel.SecondLocation == null)
                return;

            mainFrame.NavigationService.Navigate(new AddAccommodationPage(mainFrame, _user, accommodationGuidanceViewmodel.SecondLocation));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            accommodationGuidanceViewmodel.SelectionChanged();
        }

        private void CloseAccomodationButton_Click(object sender, RoutedEventArgs e) {
            AndroidYesNoDialog dialog = new AndroidYesNoDialog("Are you sure you want to close this accommodation?");
            bool? result = dialog.ShowDialog();

            if (result == false) {
                return;
            }

            if (!accommodationGuidanceViewmodel.CloseButton()) {
                mainFrame.NavigationService.Navigate(new AccommodationPage(mainFrame, _user));
            }
        }
    }
}
