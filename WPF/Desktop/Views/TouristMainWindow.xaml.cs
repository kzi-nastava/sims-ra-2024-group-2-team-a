﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window {
        private Button _currentSelectedButton;
        public int UserId { get; set; }
        public TouristMainWindow(int userId) {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.85;
            this.Height = screenHeight * 0.85;

            DataContext = this;
            UserId = userId;

            _currentSelectedButton = HomeButton;
            UpdateButtonColors();
            PageFrame.Navigate(new TouristHomePage(UserId));
        }

        private void UpdateButtonColors() {
            foreach (var button in new[] { HomeButton, ReservationsButton, CouponsButton, NotificationsButton, RequestsButton, SettingsButton }) 
                button.Background = Application.Current.Resources["DarkGreen"] as SolidColorBrush;

            if (_currentSelectedButton != null) 
                _currentSelectedButton.Background = Application.Current.Resources["Green"] as SolidColorBrush;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristHomePage(UserId));
            _currentSelectedButton = HomeButton;
            UpdateButtonColors();
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristReservationsPage(UserId));
            _currentSelectedButton = ReservationsButton;
            UpdateButtonColors();
        }

        private void CouponsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristVouchersPage(UserId));
            _currentSelectedButton = CouponsButton;
            UpdateButtonColors();
        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristNotificationsPage(UserId));
            _currentSelectedButton = NotificationsButton;
            UpdateButtonColors();
        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new RequestsPage(UserId));
            _currentSelectedButton = RequestsButton;
            UpdateButtonColors();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) {
            SettingsWindow settingsWindow = new SettingsWindow(UserId);
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }
    }
}
