using BookingApp.WPF.Android.ViewModels;
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

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for FilterForumsWindow.xaml
    /// </summary>
    public partial class FilterForumsWindow : Window {
        
        private ForumsFilterViewmodel filterViewmodel;
        public FilterForumsWindow(ForumsPageViewmodel forumsPageViewmodel) {
            InitializeComponent();
            filterViewmodel = new ForumsFilterViewmodel(forumsPageViewmodel);

            DataContext = filterViewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            filterViewmodel.ButtonClick();
            this.Close();
        }
    }
}
