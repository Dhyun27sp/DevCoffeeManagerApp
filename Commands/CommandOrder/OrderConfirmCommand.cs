﻿using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
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

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if(orderFoodViewModel.Ordereds.Count == 0)
            {
                MessageBox.Show("Bạn chưa đặt món nào xin vui lòng đặt món");
            }
            else
            {
                MessageBox.Show("Đặt Món thành công");
                SessionStatic.SetOrdereds = orderFoodViewModel.Ordereds;
                _navigationStore.CurrentViewModel = new OptionViewModel(_navigationStore);
            }
        }
    }
}
