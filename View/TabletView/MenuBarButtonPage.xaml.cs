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
    /// Interaction logic for MenuBarButtonPage.xaml
    /// </summary>
    public partial class MenuBarButtonPage : Page
    {
        private Frame menuBarFrame, mainFrame;
        public MenuBarButtonPage(Frame menuBarF, Frame mainF)
        {
            InitializeComponent();
            menuBarFrame = menuBarF;
            mainFrame = mainF;

            mainFrame.IsHitTestVisible = true;
            mainFrame.Opacity = 1;
        }

        private void menuButton_Click(object sender, RoutedEventArgs e) {
            menuBarFrame.Content = new MenuBarPage(menuBarFrame, mainFrame);

        }
    }
}
