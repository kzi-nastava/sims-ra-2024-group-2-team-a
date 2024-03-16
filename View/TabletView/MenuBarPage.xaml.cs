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

namespace BookingApp.View.TabletView
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBarPage : Page
    {
        private Frame menuBarFrame, mainFrame;
        public MenuBarPage(Frame menuBarF, Frame mainF)
        {
            InitializeComponent();
            mainFrame = mainF;
            menuBarFrame = menuBarF;
            mainFrame.IsHitTestVisible = false;
            mainFrame.Opacity = 0.6;
        }

        private void menuButton_Click(object sender, RoutedEventArgs e) {
            menuBarFrame.Content = new MenuBarButtonPage(menuBarFrame, mainFrame);
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            mainFrame.Content = new AddTourPage(mainFrame);
            menuBarFrame.Content = new MenuBarButtonPage(menuBarFrame, mainFrame);
        }
        private void removeButton_Click(object sender, RoutedEventArgs e) {

        }

        private void followLiveButton_Click(object sender, RoutedEventArgs e) {
            mainFrame.Content = new FollowLiveTourPage(mainFrame);
            menuBarFrame.Content = new MenuBarButtonPage(menuBarFrame, mainFrame);
        }

        private void statsButton_Click(object sender, RoutedEventArgs e) {

        }

        private void reviewsButton_Click(object sender, RoutedEventArgs e) {

        }

        private void requestsButton_Click(object sender, RoutedEventArgs e) {

        }
/*
        private void ShowMenuBar(bool show) {
            if (show) {
                menuBarGrid.IsHitTestVisible = true;
                menuBarGrid.Opacity = 1;
                menuBarGrid.Width = 300;
            }
            else {
                menuBarGrid.IsHitTestVisible = false;
                menuBarGrid.Opacity = 0;
                menuBarGrid.Width = 0;
            }
        }*/
    }
}
