using System;
using System.Windows.Input;

namespace ViewModel
{
    public class CommandHandler : ICommand
    {
        Action<object> _execute;
        Func<object, bool> _canExecute;

        public CommandHandler(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                //CommandManager.RequerySuggested += value;
            }
            remove
            {
                //CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return false;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}