using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

public class ToastNotificationService : IDisposable {
    private Notifier _notifier;

    public ToastNotificationService() {
        _notifier = new Notifier(cfg => {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(5),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

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
        _notifier.Dispose();
    }
}