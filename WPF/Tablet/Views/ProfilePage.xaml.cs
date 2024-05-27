using BookingApp.WPF.Tablet.ViewModels;
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

namespace BookingApp.WPF.Tablet.Views
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        ProfileViewModel ViewModel { get; set; }
        private Window _main;
        private Frame _mainFrame;
        public ProfilePage(int userId, Window main, Frame mFrame){
            InitializeComponent();
            _main   = main;
            _mainFrame = mFrame;
            ViewModel = new ProfileViewModel(userId);
            DataContext = ViewModel;
        }

        private void Quit_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Quit_Executed(object sender, ExecutedRoutedEventArgs e) {
            MessageBoxResult response = MessageBox.Show("Are you sure you want to QUIT?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (response.Equals(MessageBoxResult.No)) {
                return;
            }
            ViewModel.Quit();
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            _main.Close();
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) {
            _mainFrame.GoBack();
        }

        private void Logout_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Logout_Executed(object sender, ExecutedRoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            _main.Close();
        }
    }
}
