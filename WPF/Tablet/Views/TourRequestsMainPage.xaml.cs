﻿using BookingApp.WPF.DTO;
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
        private Frame _additionalFrame, _mainFrame;
        private int _userId;
        public GuideProfileDTO guideProfileDTO { get; set; }
        public TourRequestsMainPage(Frame mFrame, Frame aFrame, int userId) {
            Window mainWindow = Application.Current.MainWindow;
            ProfileViewModel p = (ProfileViewModel)mainWindow.DataContext;
            guideProfileDTO = p.guideProfileDTO;
            DataContext = this;
            InitializeComponent();
            _additionalFrame = aFrame;
            _mainFrame = mFrame;
            _userId = userId;
        }

       
        private void menuTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedTab = (sender as TabControl)?.SelectedItem as TabItem;
            switch (selectedTab.Header.ToString()) {
                case "Regular Tour Requests":
                    _additionalFrame.Content = new TourRequestsPage(_additionalFrame, _userId); 
                    break;

                case "Complex Tour Requests":
                    _additionalFrame.Content = new ComplexTourRequestPage(_additionalFrame, _userId);
                    break;

                case "Stats for Requests":
                    _additionalFrame.Content = new TourRequestStatsPage();
                    break;

                case "Suggestions":
                    _additionalFrame.Content = new TourRequestSuggestionPage(_mainFrame, _additionalFrame, _userId);
                    break;

                default: break;
            }
        }
        private void Help_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (guideProfileDTO.IsHelpActive) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
        }
        private void Help_Executed(object sender, ExecutedRoutedEventArgs e) {
            WizardWindow wizardWindow = new WizardWindow(_userId, 4, false);
            wizardWindow.ShowDialog();
        }
    }
}
