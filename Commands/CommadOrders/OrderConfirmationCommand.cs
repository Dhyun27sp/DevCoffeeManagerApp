using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.StaticClass;

namespace DevCoffeeManagerApp.Commands.CommadOrders
{
    public class OrderConfirmationCommand : CommandBase
    {
        private OrderFoodViewModel orderFoodViewModel;
        
        public OrderConfirmationCommand(OrderFoodViewModel orderFoodViewModel) { 
            this.orderFoodViewModel = orderFoodViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            SessionStatic.Ordereds = orderFoodViewModel.Ordereds;
        }
    }
}
