using System;
using System.Windows.Input;

namespace EX.VM.Infrostructure
{
    public class RelayCommand : ICommand
    {
        Action<object> execute; //void Execute(object parameter); - объект делегата Action<object>
        Func<object, bool> canExecute;//void Execute(object parameter); - объект делегата Func<object, bool>

        public RelayCommand(Action<object> act, Func<object, bool> f = null)
        {
            execute = act;
            canExecute = f;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {

            //    return true;
            return canExecute == null ? true : canExecute(parameter);
        }


        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
