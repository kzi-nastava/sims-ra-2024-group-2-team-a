﻿using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for MenuBarButtonPage.xaml
    /// </summary>
    public partial class MenuBarButtonPage : Page {
        private Frame _menuBarFrame, _mainFrame;
        private int _userId;
        public MenuBarButtonPage(Frame menuBarF, Frame mainF, int userId) {
            InitializeComponent();
            _menuBarFrame = menuBarF;
            _mainFrame = mainF;
            _userId = userId;
            _mainFrame.IsHitTestVisible = true;
            _mainFrame.Opacity = 1;
        }

        private void MenuBar_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void MenuBar_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            _menuBarFrame.Content = new MenuBarPage(_menuBarFrame, _mainFrame, _userId);
        }
    }
}
