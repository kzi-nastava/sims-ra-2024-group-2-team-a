using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window {
        public int UserId { get; set; }
        public TouristMainWindow(int userId) {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            this.WindowState = WindowState.Maximized;
            PageFrame.Navigate(new TouristHomePage(UserId));
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristHomePage(UserId));
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristReservationsPage(UserId));
        }

        private void CouponsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristVouchersPage(UserId));
        }
    }
}
