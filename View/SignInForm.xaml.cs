using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.AndroidViews;
using BookingApp.View.TabletView;
using BookingApp.View.WebViews;
using BookingApp.WPF.Desktop.Views;
using BookingApp.View.DesktopViews;
using BookingApp.WPF.Tablet.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.WPF.Android.Views;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    if (user.Category == UserCategory.Owner)
                    {
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                        this.Close();
                    }else if(user.Category == UserCategory.Tourist){
                        TouristMainWindow touristMainWindow = new TouristMainWindow(user.Id);
                        touristMainWindow.Show();
                        this.Close();
                    }else if(user.Category == UserCategory.Guide) {
                        GuideMainWindow guideMainWindow = new GuideMainWindow(user.Id);
                        guideMainWindow.Show();
                        this.Close();
                    }else{
                        GuestMainWindow  guestMainWindwo = new GuestMainWindow(user);
                        guestMainWindwo.Show();
                        Close();
                    }
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
