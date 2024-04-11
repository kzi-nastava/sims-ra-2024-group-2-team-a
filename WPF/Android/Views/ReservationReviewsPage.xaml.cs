using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Services;
using BookingApp.WPF.Android.ViewModels;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for ReservationReviewsPage.xaml
    /// </summary>
    public partial class ReservationReviewsPage : Page {

        public ReservationReviewsViewmodel ReservationReviewsViewmodel { get; set; }
        public ReservationReviewsPage(User user) {
            InitializeComponent();
            ReservationReviewsViewmodel = new ReservationReviewsViewmodel(user);
            
            this.DataContext = ReservationReviewsViewmodel;
        }

        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ReservationReviewsViewmodel.ListViewSelectionChanged();
        }

        private void AssignGradeButton_Click(object sender, RoutedEventArgs e) {
            ReservationReviewsViewmodel.AssignGradeButton();
        }

        private void ViewGradeButton_Click(object sender, RoutedEventArgs e) {
            ReservationReviewsViewmodel.ViewGradeButton();        }
        private void RequestsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ReservationReviewsViewmodel.RequestsListSelectionChanged();
        }

      
    }
}
