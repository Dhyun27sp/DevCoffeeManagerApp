using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandSell
{
    public class OptionCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        public OptionCommand(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new OptionOrderViewModel();
        }
    }
}
