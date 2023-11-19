﻿using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class OrderConfirmCommand : CommandBase
    {
        private OrderViewModel orderFoodViewModel;
        private readonly NavigationStore _navigationStore;
        public OrderConfirmCommand(OrderViewModel orderFoodViewModel, NavigationStore navigationStore) { 
            this.orderFoodViewModel = orderFoodViewModel;
            this._navigationStore = navigationStore;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MessageBox.Show("Đặt Món thành công");
            SessionStatic.Ordereds = orderFoodViewModel.Ordereds;
            _navigationStore.CurrentViewModel = new OptionViewModel();
        }
    }
}
