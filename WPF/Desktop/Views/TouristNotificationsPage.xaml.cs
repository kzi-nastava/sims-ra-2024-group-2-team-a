﻿using BookingApp.WPF.Desktop.ViewModels;
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

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristNotificationsPage.xaml
    /// </summary>
    public partial class TouristNotificationsPage : Page {
        private readonly TouristNotificationsPageViewModel viewModel;
        
        public TouristNotificationsPage(int userId) {
            InitializeComponent();
            viewModel = new TouristNotificationsPageViewModel(userId);
            DataContext = viewModel;
        }


    }
}
