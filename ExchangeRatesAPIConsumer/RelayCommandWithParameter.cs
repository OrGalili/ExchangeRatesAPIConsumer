using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ExchangeRatesAPIConsumer
{
    class RelayCommandWithParameter:ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommandWithParameter(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
