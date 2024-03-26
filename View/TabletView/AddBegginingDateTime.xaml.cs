using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.TabletView
{
    /// <summary>
    /// Interaction logic for AddBegginingDateTime.xaml
    /// </summary>
    public partial class AddBegginingDateTimeWindow : Window
    {
        public ObservableCollection<DateTime> begginings { get; set; }
        private DateOnly _date;
        public DateOnly Date {
            get {
                return _date;
            }
            set {
                if( _date != value ) {
                    _date = value;
                }
            }
        }
        private int _time;
        public int Time {
            get { return _time; }
            set {
                if(_time != value ) {
                    _time = value;
                }
            }
        }
        public AddBegginingDateTimeWindow(ObservableCollection<DateTime> dateTimes) {
            InitializeComponent();
            DataContext = this;

            begginings = dateTimes;
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            begginings.Add(new DateTime(Date.Year, Date.Month, Date.Day, Time, 0, 0));
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            Date = DateOnly.FromDateTime(datePicker.SelectedDate.Value);
        }
    }
}
