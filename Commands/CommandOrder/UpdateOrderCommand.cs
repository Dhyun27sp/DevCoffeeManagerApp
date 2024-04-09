using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class UpdateOrderCommand: CommandBase
    {
        private OrderViewModel orderFoodViewModel;
        public UpdateOrderCommand(OrderViewModel OrderFoodViewModel) 
        {
            this.orderFoodViewModel = OrderFoodViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (orderFoodViewModel != null)
            {
                ;
                if (parameter is DishModel datageted)
                {
                    int t = orderFoodViewModel.Ordereds.Count(item => item._id == datageted._id && item.Ice == datageted.Ice && item.Sugar == datageted.Sugar);
                    if (t > 1)
                    {
                        int count = 1;
                        int duplicate = 0;
                        foreach (var item in orderFoodViewModel.Ordereds)
                        {
                            if (item.dish_name == datageted.dish_name && item.Ice == datageted.Ice && item.Sugar == datageted.Sugar)
                            {
                                count--;
                                duplicate = item.Quantity;
                                orderFoodViewModel.Ordereds.Remove(item);
                                break;
                            }
                        }
                        if (count != 1)
                        {
                            string name = datageted.dish_name;
                            DishModel ItemOrdered = orderFoodViewModel.Ordereds.FirstOrDefault(item => item._id == datageted._id && item.Ice == datageted.Ice && item.Sugar == datageted.Sugar);
                            ItemOrdered.Quantity += duplicate;
                        }
                    }
                    
                }
                
            }
        }
    }
}
