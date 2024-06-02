using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using BookingApp.WPF.Web.Views;

public class ToastNotificationService : IDisposable {
    private Notifier _notifier;

    public ToastNotificationService() {
    }

    public void ConfigureNotifier(Window mainWindow) {
        _notifier = new Notifier(cfg => {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: mainWindow,
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(3));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }

    public void ShowSuccess(string message) {
        _notifier.ShowSuccess(message);
    }

    public void ShowInformation(string message) {
        _notifier.ShowInformation(message);
    }

    public void ShowWarning(string message) {
        _notifier.ShowWarning(message);
    }

    public void ShowError(string message) {
        _notifier.ShowError(message);
    }

    public void Dispose() {
        if(_notifier != null) {
            _notifier.Dispose(); //Pucka kod mene(Mede) i Djuke

        }
    }
}