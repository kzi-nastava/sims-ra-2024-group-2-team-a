using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
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
    /// Interaction logic for RenovationDescriptionWindow.xaml
    /// </summary>
    public partial class RenovationDescriptionWindow : Window { 
        public RenovationDescriptionWindow(AccommodationRenovationDTO renovationDTO, bool editable) {
            InitializeComponent();
            RenovationDescriptionViewmodel renovationDescriptionViewmodel = new RenovationDescriptionViewmodel(renovationDTO, this, editable);
                
            DataContext = renovationDescriptionViewmodel;
        }
    }
}
