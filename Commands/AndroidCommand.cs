using System;
using System.Windows.Input;

namespace BookingApp.Commands {
    public class AndroidCommand:ICommand {

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public AndroidCommand(Action<object> execute, Predicate<object> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return _canExecute(parameter);
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
