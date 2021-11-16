using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ExchangeRatesAPIConsumer
{
    //based on: https://www.youtube.com/watch?v=8WfD2cFRymM
    class RelayCommandNoParameter : ICommand
    {
        readonly Action _execute;
        readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommandNoParameter(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
