using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandSell
{
    internal class Add_Munix_Delete_in_Option_Command : CommandBase
    {
        public event EventHandler CanExecuteChanged;
        OptionOrderViewModel OptionOrderViewModel;
        private string sign;
        public Add_Munix_Delete_in_Option_Command(OptionOrderViewModel optionOrderViewModel, string sign)
        {
            OptionOrderViewModel = optionOrderViewModel;
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
                        OptionOrderViewModel.OrderedFood = OptionOrderViewModel.OrderedFood;
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
                            foreach (var Item in OptionOrderViewModel.OrderedFood)
                            {
                                if (Item.dish_name == datageted.dish_name)
                                {
                                    OptionOrderViewModel.OrderedFood.Remove(Item);
                                    OptionOrderViewModel.OrderedFood = OptionOrderViewModel.OrderedFood;
                                    total_money();
                                    return;
                                }
                            }
                        }
                    }
                    OptionOrderViewModel.OrderedFood = OptionOrderViewModel.OrderedFood;
                }
                else if (sign == "Delete")
                {
                    foreach (var Item in OptionOrderViewModel.OrderedFood)
                    {
                        if (Item.dish_name == datageted.dish_name)
                        {
                            OptionOrderViewModel.OrderedFood.Remove(Item);
                            OptionOrderViewModel.OrderedFood = OptionOrderViewModel.OrderedFood;
                            total_money();
                            return;
                        }
                    }
                }
            }
        }
        private void total_money()
        {
            int total = 0;
            if (OptionOrderViewModel.OrderedFood != null)
            {
                foreach (var Ordd in OptionOrderViewModel.OrderedFood)
                {
                    foreach (var dish in OptionOrderViewModel.OrderedFood)
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
            OptionOrderViewModel.Total = total.ToString();
        }


    }
}
