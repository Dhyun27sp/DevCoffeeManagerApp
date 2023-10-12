using System;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    internal class RelayCommandBase
    {
        public event EventHandler CanExecuteChanged;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}