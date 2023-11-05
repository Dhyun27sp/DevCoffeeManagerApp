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
    public class ReserveCommand: CommandBase
    {
        private OrderFoodViewModel OrderFoodViewModel;
        public ReserveCommand(OrderFoodViewModel OrderFoodViewModel)
        {
            this.OrderFoodViewModel = OrderFoodViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            int make = 0;
            int total = 0;
            if (parameter is DishModel Dish)
            {
                foreach (var item in OrderFoodViewModel.Ordereds)
                {
                    if (item.dish_name == Dish.dish_name)
                    {
                        make = 1;
                        break;
                    }
                }
                if (make != 1)
                {
                    string name = Dish.dish_name;
                    string Quanlity = "1";
                    DishModel ItemOrdered = new DishModel(Dish._id, name, Quanlity, Dish.Saleprice, Dish.price);
                    OrderFoodViewModel.Ordereds.Add(ItemOrdered);
                }
                else
                {
                    int result;
                    foreach (var item in OrderFoodViewModel.Ordereds)
                    {
                        if (item.dish_name == Dish.dish_name)
                        {
                            if (int.TryParse(item.Quantity, out result))
                            {
                                result += 1;
                                item.Quantity = result.ToString();
                                break;
                            }
                        }

                    }
                }
            }
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
