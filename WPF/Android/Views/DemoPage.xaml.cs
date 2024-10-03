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

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for DemoPage.xaml
    /// </summary>
    public partial class DemoPage : Page {
        
        Frame DemoFrame { get; set; }
        public DemoPage(Frame demoFrame) {
            InitializeComponent();
            DemoFrame = demoFrame;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            DemoFrame.Content = null;
        }
    }
}
