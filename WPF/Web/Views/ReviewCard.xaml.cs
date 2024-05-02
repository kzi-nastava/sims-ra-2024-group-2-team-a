using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
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

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ReviewCard.xaml
    /// </summary>
    public partial class ReviewCard : UserControl {

        public ReviewCardViewModel ViewModel { get; set; }

        public ReviewCard() {
            InitializeComponent();
        }

    }
}
