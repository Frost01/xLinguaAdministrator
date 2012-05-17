using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Views.SL.ViewModels
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Predicate<Object> _canExecute = null;
        Action<Object> _executeAction = null;


        public RelayCommand(Action<object> executeAction, Predicate<Object> canExecute = null)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }


        public void UpdateCanExecute()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }


        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);

            return true;
        }


        public void Execute(object parameter)
        {
            if (_executeAction != null)
                _executeAction(parameter);
        }
    }
}
