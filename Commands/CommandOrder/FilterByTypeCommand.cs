using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class FilterByTypeCommand : CommandBase
    {
        private OrderViewModel orderFoodViewModel;
        public FilterByTypeCommand(OrderViewModel orderFoodViewModel)
        {
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
                List<DishModel> Dishs_Search = new List<DishModel>();// Danh sách món nạp mấy món đủ điều kiện vào
                if (types_dish_search != "")// nếu mà search nó khác rỗng
                {
                    foreach (var item in AllDishsVariable) // lọc danh sách tất cả các món
                    {
                        string itemconvertUnicode = ConvertString(item.dish_name).Replace(" ", "");
                        string types_dish_searchconvertUnicode = ConvertString(types_dish_search).Replace(" ", "");
                        bool contains = itemconvertUnicode.Contains(types_dish_searchconvertUnicode);
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
                                    else if(Type_Special == "New Dish")
                                    {
                                        if (item.newDish == true)
                                        {
                                            Dishs_Search.Add(item);
                                        }
                                    }
                                    else if (Type_Special == "Hot Dish")
                                    {
                                        if (item.HotDish == true)
                                        {
                                            Dishs_Search.Add(item);
                                        }
                                    }
                                    else
                                    {
                                        Dishs_Search.Add(item);
                                    }
                                }
                            } // kiểm tra nếu nó không phải loại ko phải tất cả
                            else
                            {
                                if (Type_Special == "Discounted")
                                {
                                    if (item.SaleDish == true)
                                    {
                                        Dishs_Search.Add(item);
                                    }
                                }
                                else if (Type_Special == "New Dish")
                                {
                                    if (item.newDish == true)
                                    {
                                        Dishs_Search.Add(item);
                                    }
                                }
                                else if (Type_Special == "Hot Dish")
                                {
                                    if (item.HotDish == true)
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
                        
                    }
                    if (Dishs_Search.Count == 0)
                    {
                        return null;
                    }
                }
                else // search là rỗng  
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
                            return sort_special_type_comesfirst(combine, Type_Special);
                        }
                        else if (Type_Special == "New Dish")
                        {
                            List<DishModel> combine = new List<DishModel>();
                            List<DishModel> notdishnew = new List<DishModel>();
                            notdishnew = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                            combine.AddRange(Dishs_Search);
                            combine.AddRange(notdishnew);
                            return sort_special_type_comesfirst(combine, Type_Special);
                        }
                        else if (Type_Special == "Hot Dish")
                        {
                            List<DishModel> combine = new List<DishModel>();
                            List<DishModel> notdishHot = new List<DishModel>();
                            notdishHot = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                            combine.AddRange(Dishs_Search);
                            combine.AddRange(notdishHot);
                            return sort_special_type_comesfirst(combine, Type_Special);
                        }
                        return AllDishsVariable;
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
                            return sort_special_type_comesfirst(combine, Type_Special);
                        }
                        else if (Type_Special == "New Dish")
                        {
                            List<DishModel> combine = new List<DishModel>();
                            List<DishModel> notdishnew = new List<DishModel>();
                            notdishnew = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                            combine.AddRange(Dishs_Search);
                            combine.AddRange(notdishnew);
                            return sort_special_type_comesfirst(combine, Type_Special);
                        }
                        else if (Type_Special == "Hot Dish")
                        {
                            List<DishModel> combine = new List<DishModel>();
                            List<DishModel> notdishHot = new List<DishModel>();
                            notdishHot = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                            combine.AddRange(Dishs_Search);
                            combine.AddRange(notdishHot);
                            return sort_special_type_comesfirst(combine, Type_Special);
                        }
                        return AllDishsVariable;

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
        private List<DishModel> sort_special_type_comesfirst(List<DishModel> special_type_comesfirst, string special_type)
        {
            List<DishModel> sortedList = new List<DishModel>();
            if (special_type == "Discounted")
            {
                sortedList = special_type_comesfirst
                .OrderByDescending(x => x.SaleDish)
                .ToList();
            }
            else if (special_type == "New Dish")
            {
                sortedList = special_type_comesfirst
                .OrderByDescending(x => x.newDish)
                .ToList();
            }
            else if (special_type == "Hot Dish")
            {
                sortedList = special_type_comesfirst
                .OrderByDescending(x => x.HotDish)
                .ToList();
            }
            
            return sortedList;
            
        }
        public string ConvertString(string stringInput)
        {
            stringInput = stringInput.ToLower();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                stringInput = stringInput.Replace(convert[i], To[i]);
            }
            return stringInput;
        }

    }
}
