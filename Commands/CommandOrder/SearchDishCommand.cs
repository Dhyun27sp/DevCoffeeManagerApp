using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class SearchDishCommand : CommandBase
    {
        private OrderFoodViewModel orderFoodViewModel;
        private FilterByTypeCommand _changeTypeDishCommand;
        public SearchDishCommand(OrderFoodViewModel orderFoodViewModel) {
           this.orderFoodViewModel = orderFoodViewModel;
            _changeTypeDishCommand = new FilterByTypeCommand(orderFoodViewModel);
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            orderFoodViewModel.Dishs = _changeTypeDishCommand.filter(orderFoodViewModel.AllDishsVariable, orderFoodViewModel.types_dish_search, orderFoodViewModel.Type, orderFoodViewModel.Type_Special);
        }
    }
}
