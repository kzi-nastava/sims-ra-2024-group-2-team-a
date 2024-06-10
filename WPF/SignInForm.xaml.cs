using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.Android.Views;
using BookingApp.WPF.Desktop.Views;
using BookingApp.WPF.Tablet.Views;
using BookingApp.WPF.Web.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF {
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window {

        private readonly UserService _userService = ServicesPool.GetService<UserService>();
        private readonly GuideService _guideService = ServicesPool.GetService<GuideService>();

        private string _username;
        public string Username {
            get => _username;
            set {
                if (value != _username) {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public SignInForm() {
            InitializeComponent();
            DataContext = this;
        }

        private void SignIn(object sender, RoutedEventArgs e) {
            User user = _userService.GetByUsername(Username);

            if (user == null) {
                MessageBox.Show("Wrong username or password!");
                return;
            }

            if (user.Password != passwordBox.Password) {
                MessageBox.Show("Wrong username or password!");
                return;
            }

            if (user.Category == UserCategory.Owner) {
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                Close();
            }
            else if (user.Category == UserCategory.Tourist) {
                TouristMainWindow touristMainWindow = new TouristMainWindow(user.Id);
                App.NotificationService.ConfigureNotifier(touristMainWindow);
                touristMainWindow.Show();
                Close();
            }
            else if (user.Category == UserCategory.Guide) {
                if (_guideService.GetById(user.Id).IsFirstTime) {
                    WizardWindow wizardWindow = new WizardWindow(user.Id, 1, true);
                    wizardWindow.Show();
                    Close();
                }
                else {
                    GuideMainWindow guideMainWindow = new GuideMainWindow(user.Id);
                    guideMainWindow.Show();
                    Close();
                }
            }
            else {
                GuestMainWindow guestMainWindow = new GuestMainWindow(user.Id);
                App.GuestMainWindowReference = guestMainWindow;
                App.NotificationService.ConfigureNotifier(guestMainWindow);
                guestMainWindow.Show();
                Close();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
