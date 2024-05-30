using BookingApp.Domain.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AllRenovationsPage.xaml
    /// </summary>
    public partial class AllRenovationsPage : Page, IDemo {

        public AllRenovationsViewmodel AllRenovationsViewmodel;
        public AllRenovationsPage(User user) {
            InitializeComponent();
            
            AllRenovationsViewmodel = new AllRenovationsViewmodel(user);
            DataContext = AllRenovationsViewmodel;
        }

        public void StartDemo() {
            AllRenovationsViewmodel.StartDemo();
        }

        public void StopDemo() {
            AllRenovationsViewmodel.StopDemo();
        }
    }
}
