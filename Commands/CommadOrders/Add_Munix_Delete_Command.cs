using DevCoffeeManagerApp.Component.ComPonentOrder;
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
            if (parameter is StructOfOrderedItem datageted)
            {
                string inputString = datageted.Quantity;
                int result;
                if (sign == "Plus")
                {
                    if (int.TryParse(inputString, out result))
                    {
                        result += 1;
                        datageted.Quantity = result.ToString();
                    }
                }
                else if (sign == "Minus")
                {

                    if (int.TryParse(inputString, out result))
                    {
                        result -= 1;
                        datageted.Quantity = result.ToString();

                        if (result == 0)
                        {
                            foreach (var Item in OrderFoodViewModel.Ordereds)
                            {
                                if (Item.Name_Dish == datageted.Name_Dish)
                                {
                                    OrderFoodViewModel.Ordereds.Remove(Item);
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
                        if (Item.Name_Dish == datageted.Name_Dish)
                        {
                            OrderFoodViewModel.Ordereds.Remove(Item);
                            return;
                        }
                    }
                }
            }

            if (sign == "DeleteAll")
            {
                OrderFoodViewModel.Ordereds.Clear();
            }
        }

    }
}
