using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class AddDishCommand: CommandBase
    {
        private OrderViewModel OrderFoodViewModel;
        public AddDishCommand(OrderViewModel OrderFoodViewModel)
        {
            this.OrderFoodViewModel = OrderFoodViewModel;
        }

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
                    int Quanlity = 1;
                    int? saleprice = Dish.Saleprice;
                    DishModel ItemOrdered = new DishModel(Dish._id, name, Quanlity, saleprice, Dish.price);
                    OrderFoodViewModel.Ordereds.Add(ItemOrdered);                    
                }
                else
                {
                    int result;
                    foreach (var item in OrderFoodViewModel.Ordereds)
                    {
                        if (item.dish_name == Dish.dish_name)
                        {
                            if (int.TryParse((item.Quantity).ToString(), out result))
                            {
                                result += 1;
                                item.Quantity = result;
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
                                total = total + (int)dish.Saleprice * Ordd.Quantity;
                            }
                            else
                            {
                                total = total + dish.price.Value * Ordd.Quantity;
                            }

                        }
                    }
                }
            }
            OrderFoodViewModel.Total = total;
        }
    }
}
