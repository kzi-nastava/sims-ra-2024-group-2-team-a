using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for AddBegginingDateTime.xaml
    /// </summary>
    public partial class AddBegginingDateTimeWindow : Window
    {
        public ObservableCollection<DateTime> begginings { get; set; }
        public DateOnly Date { get; set; }
        public int Time { get; set; }
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
