﻿using BookingApp.Domain.Model;
using BookingApp.WPF.Desktop.ViewModels;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for CreateComplexRequestWindow.xaml
    /// </summary>
    public partial class CreateComplexRequestWindow : Window {
        int UserId { get; set; }
        private RequestsPageViewModel _parentViewModel;
        public CreateComplexRequestWindowViewModel ViewModel { get; set; }
        public CreateComplexRequestWindow(int userId, RequestsPageViewModel requestsPageViewModel) {
            InitializeComponent();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.8;
            this.Height = screenHeight * 0.8;

            UserId = userId;
            _parentViewModel = requestsPageViewModel;
            ViewModel = new CreateComplexRequestWindowViewModel(UserId, requestsPageViewModel);
            ViewModel.CloseAction = new Action(this.Close);
            this.DataContext = ViewModel;
        }

        private void AddSimpleRequestButton_Click(object sender, RoutedEventArgs e) {
            CreateRequestWindow window = new CreateRequestWindow(UserId, ViewModel, _parentViewModel);          
            window.ShowDialog();
        }
    }
}
