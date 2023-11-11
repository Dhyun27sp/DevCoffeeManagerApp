using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Services.ServiceOrder
{
    internal interface IOrderService
    {
        List<string> load_types_dish();
        List<string> loadSpecials();
        List<DishModel> LoadAllDish();
        List<DishModel> LoadDishWithType(List<DishModel> AllDishsVariable, string Type);
        List<DishModel> LoadOnlydiscountfortype(List<DishModel> AllDishsVariable, string Type_Special, string Type);
        List<DishModel> filter(List<DishModel> AllDishsVariable, string types_dish_search, string Type, string Type_Special);
    }
}
