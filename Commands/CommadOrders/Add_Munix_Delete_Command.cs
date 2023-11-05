using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.Commands.CommadOrders
{
    internal class Add_Munix_Delete_Command:CommandBase
    {
        public event EventHandler CanExecuteChanged;
        OrderFoodViewModel OrderFoodViewModel;
        private string sign;
        public Add_Munix_Delete_Command(OrderFoodViewModel orderFoodViewModel, string sign)
        {
            OrderFoodViewModel = orderFoodViewModel;
            this.sign = sign;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            
            if (parameter is DishModel datageted)
            {
                string inputString = datageted.Quantity;
                int result;
                if (sign == "Plus")
                {
                    if (int.TryParse(inputString, out result))
                    {
                        result += 1;
                        datageted.Quantity = result.ToString();
                        total_money();
                    }
                }
                else if (sign == "Minus")
                {
                    if (int.TryParse(inputString, out result))
                    {
                        result -= 1;
                        datageted.Quantity = result.ToString();
                        total_money();
                        if (result == 0)
                        {
                            foreach (var Item in OrderFoodViewModel.Ordereds)
                            {
                                if (Item.dish_name == datageted.dish_name)
                                {
                                    OrderFoodViewModel.Ordereds.Remove(Item);
                                    total_money();
                                    return;
                                }
                            }
                        }
                    }
                }
                else if (sign == "Delete")
                {
                    foreach (var Item in OrderFoodViewModel.Ordereds)
                    {
                        if (Item.dish_name == datageted.dish_name)
                        {
                            OrderFoodViewModel.Ordereds.Remove(Item);
                            total_money();
                            return;
                        }
                    }
                }
            }

            if (sign == "DeleteAll")
            {
                OrderFoodViewModel.Ordereds.Clear();
                total_money();
            }
        }
        private void total_money()
        {
            int total = 0;
            if (OrderFoodViewModel.Ordereds != null)
            {
                foreach (var Ordd in OrderFoodViewModel.Ordereds)
                {
                    foreach (var dish in OrderFoodViewModel.AllDishsVariable)
                    {
                        if (dish.dish_name == Ordd.dish_name)
                        {
                            if (dish.Saleprice != null)
                            {
                                total = total + int.Parse(dish.Saleprice) * int.Parse(Ordd.Quantity);
                            }
                            else
                            {
                                total = total + (dish.price.Value * int.Parse(Ordd.Quantity));
                            }

                        }
                    }
                }
            }
            OrderFoodViewModel.Total = total.ToString();
        }

    }
}
