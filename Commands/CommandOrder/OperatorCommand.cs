using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    internal class OperatorCommand : CommandBase
    {
        OptionViewModel optionOrderViewModel;
        OrderViewModel orderFoodViewModel;
        private string sign;
        public OperatorCommand(OptionViewModel optionOrderViewModel, string sign)
        {
            this.optionOrderViewModel = optionOrderViewModel;
            this.sign = sign;
        }
        public OperatorCommand(OrderViewModel orderFoodViewModel, string sign)
        {
            this.orderFoodViewModel = orderFoodViewModel;
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
                if (optionOrderViewModel != null)
                    switch (sign)
                    {
                        case "Plus":
                            datageted.Quantity++;
                            total_money(datageted);
                            return;
                        case "Minus":
                            datageted.Quantity--;
                            if (datageted.Quantity == 0)
                            {
                                foreach (var Item in optionOrderViewModel.OrderedFood)
                                {
                                    if (Item.dish_name == datageted.dish_name)
                                    {
                                        optionOrderViewModel.OrderedFood.Remove(Item);
                                        break;
                                    }
                                }
                            }
                            total_money(datageted);
                            return;
                        case "Delete":
                            foreach (var Item in optionOrderViewModel.OrderedFood)
                            {
                                if (Item.dish_name == datageted.dish_name)
                                {
                                    optionOrderViewModel.OrderedFood.Remove(Item);
                                    total_money(datageted);
                                    break;
                                }
                            }
                            return;
                    }
                else
                    switch (sign)
                    {
                        case "Plus":
                            datageted.Quantity++;
                            total_money(datageted);
                            return;
                        case "Minus":
                            datageted.Quantity--;
                            if (datageted.Quantity == 0)
                            {
                                foreach (var Item in orderFoodViewModel.Ordereds)
                                {
                                    if (Item.dish_name == datageted.dish_name)
                                    {
                                        orderFoodViewModel.Ordereds.Remove(Item);
                                        break;
                                    }
                                }
                            }
                            total_money(datageted);
                            return;
                        case "Delete":
                            foreach (var Item in orderFoodViewModel.Ordereds)
                            {
                                if (Item.dish_name == datageted.dish_name)
                                {
                                    orderFoodViewModel.Ordereds.Remove(Item);
                                    total_money(datageted);
                                    break;
                                }
                            }
                            return;
                    }
            }
            else
            {
                orderFoodViewModel.Ordereds.Clear();
                total_money(null);
                return;
            }
        }
        private void total_money(DishModel datageted)
        {
            if (optionOrderViewModel != null)
            {
                if (optionOrderViewModel.OrderedFood != null)
                {
                    int total = optionOrderViewModel.Total;
                    int count = datageted.Quantity; // số lượng món ăn mới ( số lượng của món đang được select)
                    if (count == 0)
                        count = 1; // thực hiện chức năng trừ xuống 0 <=> xóa sản phẩm có SL = 1
                    int newAmount = 0; // số tiền đã cập nhật của món ăn đó ( = 0 nếu món đó bị xóa)

                    // Tính giá trị cho newAmount
                    if (datageted.Saleprice != null)
                        newAmount = (int)datageted.Saleprice * count;
                    else
                        newAmount = (int)datageted.price * count;

                    // Cập nhật lại Amount trong OrderedFood và Total
                    foreach (var dish in optionOrderViewModel.OrderedFood)
                    {
                        if (datageted.dish_name == dish.dish_name)
                        {
                            total += newAmount - dish.Amount; // thêm giá được cập nhật xóa giá cũ                        
                            count = 0;
                            break;
                        }
                    }

                    // count vẫn lớn hơn 0 tức ko có món trong OrderedFood => đang thực chức năng xóa
                    if (count > 0)
                    {
                        total -= newAmount;
                    }
                    optionOrderViewModel.Total = total;
                    optionOrderViewModel.OrderedFood = optionOrderViewModel.OrderedFood;
                    optionOrderViewModel.PlusPoint = (int)(optionOrderViewModel.Total * 0.01);
                }
            }
            else
            {
                int total = 0;
                if (orderFoodViewModel.Ordereds != null)
                {
                    foreach (var dish in orderFoodViewModel.Ordereds)
                    {

                        if (dish.Saleprice != null)
                        {
                            total += (int)dish.Saleprice * dish.Quantity;
                        }
                        else
                        {
                            total += (int)dish.price * dish.Quantity;
                        }
                    }
                }
                orderFoodViewModel.Total = total;
            }
        }
    }
}
