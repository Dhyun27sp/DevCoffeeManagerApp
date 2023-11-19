using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandSell
{
    public class TableCommand : CommandBase
    {
        private NavigationStore _navigationStore = new NavigationStore();
        public TableCommand (NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new TableViewModel(_navigationStore);
        }
    }
}
