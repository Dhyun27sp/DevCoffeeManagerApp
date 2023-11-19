using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.Store;

namespace DevCoffeeManagerApp.Commands.CommandSell
{
    public class OrderCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        public OrderCommand(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new OrderFoodViewModel(_navigationStore);
        }
    }
}
