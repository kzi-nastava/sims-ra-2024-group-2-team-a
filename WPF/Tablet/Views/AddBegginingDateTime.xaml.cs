using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            Date = DateOnly.FromDateTime(datePicker.SelectedDate.Value);
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e) {
            this.Close();
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (Date != null || Time != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void Add_Executed(object sender, ExecutedRoutedEventArgs e) {
            begginings.Add(new DateTime(Date.Year, Date.Month, Date.Day, Time, 0, 0));
            this.Close();
        }
    }
}
