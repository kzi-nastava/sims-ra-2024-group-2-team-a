﻿using BookingApp.DTO;
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

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for AccommodationCard.xaml
    /// </summary>
    public partial class AccommodationCard : UserControl {
        public AccommodationCard() {
            InitializeComponent();
        }

        private void AccommodationCardClick(object sender, MouseButtonEventArgs e) {
            Frame mainFrame = (Frame) Window.GetWindow(this).FindName("mainFrame");
            AccommodationDTO accommodationDTO = (AccommodationDTO) DataContext;
            mainFrame.Content = new CreateReservationPage(accommodationDTO);
        }
    }
}
