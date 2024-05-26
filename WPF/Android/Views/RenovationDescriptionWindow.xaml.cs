using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using Syncfusion.Windows.Shared;
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
using System.Windows.Threading;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for RenovationDescriptionWindow.xaml
    /// </summary>
    public partial class RenovationDescriptionWindow : Window { 
        public RenovationDescriptionWindow(AccommodationRenovationDTO renovationDTO, bool editable, bool demoActive) {
            InitializeComponent();
            RenovationDescriptionViewmodel renovationDescriptionViewmodel = new RenovationDescriptionViewmodel(renovationDTO, this, editable);
                
            DataContext = renovationDescriptionViewmodel;
            if (demoActive) {
                int cnt = 0;
                DispatcherTimer _timer;
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += (sender, e) =>
                {
                    if (cnt++ == 0) {
                        Color color = (Color)ColorConverter.ConvertFromString("#5a8c6b");
                        ConfirmButton.Background = new SolidColorBrush(color);
                    }
                    else
                        this.Close();
                };
                _timer.Start();
            }
        }
    }
}
