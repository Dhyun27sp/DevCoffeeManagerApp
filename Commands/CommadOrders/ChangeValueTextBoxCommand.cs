﻿using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommadOrders
{
    public class ChangeValueTextBoxCommand : CommandBase
    {
        private OrderFoodViewModel orderFoodViewModel;
        private ChangeTypeDishCommand _changeTypeDishCommand;
        public ChangeValueTextBoxCommand(OrderFoodViewModel orderFoodViewModel) {
           this.orderFoodViewModel = orderFoodViewModel;
            _changeTypeDishCommand = new ChangeTypeDishCommand(orderFoodViewModel);
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