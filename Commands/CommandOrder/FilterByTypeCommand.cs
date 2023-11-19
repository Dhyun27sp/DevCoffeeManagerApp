using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class FilterByTypeCommand :CommandBase
    {
        private OrderFoodViewModel orderFoodViewModel;
        public FilterByTypeCommand(OrderFoodViewModel orderFoodViewModel) { 
            this.orderFoodViewModel = orderFoodViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            orderFoodViewModel.Dishs = filter(orderFoodViewModel.AllDishsVariable, orderFoodViewModel.types_dish_search, orderFoodViewModel.Type, orderFoodViewModel.Type_Special);
        }
        public List<DishModel> filter(List<DishModel> AllDishsVariable, string types_dish_search, string Type, string Type_Special)
        {
            List<DishModel> Dishs_Search = new List<DishModel>();
            if (types_dish_search != "")
            {
                foreach (var item in AllDishsVariable)
                {
                    bool contains = item.dish_name.IndexOf(types_dish_search, StringComparison.OrdinalIgnoreCase) >= 0;
                    if (contains)
                    {
                        if (Type != "All Dishs")
                        {
                            if (item.category == Type)
                            {
                                if (Type_Special == "Discounted")
                                {
                                    if (item.SaleDish == true)
                                    {
                                        Dishs_Search.Add(item);
                                    }
                                }
                                else
                                {
                                    Dishs_Search.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (Type_Special == "Discounted")
                            {
                                if (item.SaleDish == true)
                                {
                                    Dishs_Search.Add(item);
                                }
                            }
                            else
                            {
                                Dishs_Search.Add(item);
                            }
                        }
                    }
                    else
                    {
                        if (AllDishsVariable.IndexOf(item) == AllDishsVariable.Count)
                        {
                            if (Dishs_Search.Count == 0)
                            {
                                return null;
                            }
                        }
                    }
                }
                if (Dishs_Search.Count > 0)
                {
                    if(Type_Special == "Discounted")
                    {
                        List<DishModel> combine = new List<DishModel>();
                        List<DishModel> notdishcount = new List<DishModel>();
                        notdishcount = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                        combine.AddRange(Dishs_Search);
                        combine.AddRange(notdishcount);
                        return sortdiscountcomesfirst(combine);
                    }
                    return Dishs_Search;
                }
            }
            else
            {
                AllDishsVariable = orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type);
                if (Type != "All Dishs")
                {
                    if (Type_Special == "Discounted")
                    {
                        List<DishModel> combine = new List<DishModel>();
                        List<DishModel> notdishcount = new List<DishModel>();
                        notdishcount = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                        combine.AddRange(Dishs_Search);
                        combine.AddRange(notdishcount);
                        return sortdiscountcomesfirst(combine);
                    }
                }
                else
                {
                    if (Type_Special == "Discounted")
                    {
                        List<DishModel> combine = new List<DishModel>();
                        List<DishModel> notdishcount = new List<DishModel>();
                        notdishcount = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                        combine.AddRange(Dishs_Search);
                        combine.AddRange(notdishcount);
                        return sortdiscountcomesfirst(combine);
                    }
                }
                return orderFoodViewModel.LoadOnlydiscountfortype(AllDishsVariable, Type_Special, Type);
            }
            return Dishs_Search;
        }
        private List<DishModel> DeleteduplicatesDish(List<DishModel> ListOnlyDisCont, List<DishModel> ListonlyType)
        {
            List<DishModel> result = new List<DishModel>();

            result = ListonlyType.Where(item => !ListOnlyDisCont.Contains(item)).ToList();
            return result;
        }
        private List<DishModel> sortdiscountcomesfirst(List<DishModel> discountcomesfirst)
        {
            List<DishModel> sortedList = discountcomesfirst
                .OrderByDescending(x => x.SaleDish)
                .ToList();
            return sortedList;
        }

    }
}
