using BookingApp.WPF.Web.Views;
using System.Windows;

namespace BookingApp {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ToastNotificationService NotificationService { get; private set; }

        public static GuestMainWindow GuestMainWindowReference { get; set; }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            NotificationService = new ToastNotificationService();
        }

        protected override void OnExit(ExitEventArgs e) {
            NotificationService.Dispose();
            base.OnExit(e);
        }
    }
}
