﻿using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandMainStaff
{
    public class OrderCommand : CommandBase
    {
        OrderFoodViewModel orderFoodViewModel = new OrderFoodViewModel();
        private MainStaffViewModel mainStaffViewModel;
        public OrderCommand(MainStaffViewModel mainStaffViewModel)
        {
            this.mainStaffViewModel = mainStaffViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            mainStaffViewModel.CurrentViewModel = orderFoodViewModel;
        }
    }
}