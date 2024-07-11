using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class SearchCommand : CommandBase
    {
        private OrderViewModel orderFoodViewModel;
        public SearchCommand(OrderViewModel orderFoodViewModel)
        {
            this.orderFoodViewModel = orderFoodViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            orderFoodViewModel.Dishs = filter(orderFoodViewModel.AllDishs, orderFoodViewModel.Search, orderFoodViewModel.Type, orderFoodViewModel.Type_Special);
        }

        // hàm fiter cho search, loại món, loại món đặt biệt
        public List<DishModel> filter(List<DishModel> AllDishs, string Search, string Type, string Tag)
        {
            List<DishModel> Dishs_Search = new List<DishModel>();
            if (Search != "")
            {
                foreach (var item in AllDishs)
                {
                    bool contains = SearchMethod.Search_have_key(item.dish_name, Search);

                    if (contains)// trường hợp key có trong món ăn
                    {
                        Dishs_Search.Add(item);
                    }

                }
                if (Dishs_Search.Count == 0)//trương hợp key ko có trong món nào
                {
                    return null;
                }
                else
                {
                    Dishs_Search = LoadDishWithType(Dishs_Search, Type);
                    return sort_tag_type_comesfirst(Dishs_Search, Tag);
                }
            }
            else // trương hợ search rỗng 
            {
                AllDishs = LoadDishWithType(AllDishs, Type);
                return sort_tag_type_comesfirst(AllDishs, Tag);
            }
        }

        public List<DishModel> LoadDishWithType(List<DishModel> AllDishs, string Type) // 
        {
            List<DishModel> DishsLocal = new List<DishModel>();
            if (Type == "All Dishs")
            {
                return AllDishs;
            }
            else
            {
                if (Type != null)
                {
                    foreach (var dish in AllDishs)
                    {
                        if (dish.category == Type)
                        {
                            DishsLocal.Add(dish);
                        }
                    }
                }
                return DishsLocal;
            }
        }
        private List<DishModel> sort_tag_type_comesfirst(List<DishModel> tag_type_comesfirst, string tag_type)
        {
            List<DishModel> sortedList = new List<DishModel>(); //sắp xếp thứ tự món theo tag
            if (tag_type == "Discounted")
            {
                sortedList = tag_type_comesfirst
                .OrderByDescending(x => x.SaleDish)
                .ToList();
            }
            else if (tag_type == "New Dish")
            {
                sortedList = tag_type_comesfirst
                .OrderByDescending(x => x.newDish)
                .ToList();
            }
            else if (tag_type == "Hot Dish")
            {
                sortedList = tag_type_comesfirst
                .OrderByDescending(x => x.HotDish)
                .ToList();
            }
            else
            {
                sortedList = tag_type_comesfirst;
            }

            return sortedList;

        }



    }
}
