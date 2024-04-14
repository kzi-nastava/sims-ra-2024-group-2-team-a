using System;
using System.Windows.Input;

public class RelayCommand : ICommand {
    private readonly Action<object> _execute;
    private readonly Func<bool> _canExecute;

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    public RelayCommand(Action<object> execute, Func<bool> canExecute = null) {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
    public event EventHandler CanExecuteChanged {
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public bool CanExecute(object parameter) {
        return _canExecute == null || _canExecute();
    }
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public void Execute(object parameter) {
        _execute(parameter);
    }
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
}
