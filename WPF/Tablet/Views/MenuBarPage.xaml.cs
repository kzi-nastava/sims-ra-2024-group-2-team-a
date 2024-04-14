﻿using BookingApp.Model;
using BookingApp.View.TabletView;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBarPage : Page {
        private Frame _menuBarFrame, _mainFrame;
        private int _userId;

        public MenuBarPage(Frame menuBarF, Frame mainF, int userId) {
            InitializeComponent();
            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;
            _mainFrame.IsHitTestVisible = false;
            _mainFrame.Opacity = 0.6;          

        }

        private void menuButton_Click(object sender, RoutedEventArgs e) {
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void homeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new ScheduledToursPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new AddTourPage(_mainFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void followLiveButton_Click(object sender, RoutedEventArgs e) {
            MenuBarViewModel viewModel = new MenuBarViewModel(_userId);
            if(viewModel.IsTourActive())
                _mainFrame.Content = new LiveTourPage(viewModel.tourDTO, _mainFrame,  _userId);
            else
                _mainFrame.Content = new FollowLiveTourPage(_userId);
            
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }
        private void finishedToursButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new FinishedToursPage(_userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }
        private void statsButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new TourStatsPage(null, _mainFrame, _menuBarFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }

        private void requestsButton_Click(object sender, RoutedEventArgs e) {

            MessageBox.Show("Pay 5$ to Unlock", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        }
    }
}
