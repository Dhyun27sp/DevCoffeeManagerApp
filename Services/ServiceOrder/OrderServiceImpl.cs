using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Services.ServiceOrder
{
    public class OrderServiceImpl
    {
        MenuDAO menuDao = new MenuDAO();
        DiscountDAO discountDAO = new DiscountDAO();
        public List<string> load_types_dish()
        {
            ObservableCollection<MenuModel> menuModels = menuDao.ReadAll_Type_dish();
            List<string> temp = new List<string>();
            temp.Add("All Dishs");
            foreach (var item in menuModels)
            {
                temp.Add(item.type_of_dish);
            }
            return temp;
        }

        public List<string> loadSpecials()
        {
            List<string>  Specials = new List<string>();
            Specials.Add("Out Spectial");
            Specials.Add("New Dish");
            Specials.Add("Hot Dish");
            Specials.Add("Discounted");

            return Specials;
        }

        public List<DishModel> LoadAllDish()
        {
            List<DishModel> DishsLocal = new List<DishModel>();
            foreach (var type in load_types_dish())
            {
                List<DishModel> DishsTemp = new List<DishModel>();
                if (type != "All Dishs")
                {
                    DishsTemp = menuDao.ReadAll(type).dish;
                    foreach (var dishtemp in DishsTemp)
                    {
                        dishtemp.category = type;
                    }
                }
                DishsLocal.AddRange(DishsTemp);
            }

            List<MenuModel> DiscountMenu = new List<MenuModel>();
            DiscountMenu = discountDAO.ListMenuDiscount();
            DiscountMenu = DeleteduplicatesMenu(DiscountMenu);
            foreach (var menu in DiscountMenu)
            {
                foreach (var dish in DishsLocal)
                {
                    if (dish.category == menu.type_of_dish)
                    {
                        List<string> listpricediscountmenu = new List<string>();
                        dish.SaleDish = true;
                        listpricediscountmenu = discountDAO.MutiPriceSaleWithMenu(menu.id);
                        listpricediscountmenu = Drop_MoneyToPercent(listpricediscountmenu);
                        foreach (var lpdcm in listpricediscountmenu)
                        {
                            double convertsaleprice = Double.Parse(lpdcm);
                            if (0 < convertsaleprice && convertsaleprice <= 1)
                            {
                                if (dish.Saleprice == null)
                                {
                                    dish.Saleprice = (dish.price - dish.price * convertsaleprice).ToString();
                                }
                                else
                                {
                                    dish.Saleprice = (int.Parse(dish.Saleprice) - int.Parse(dish.Saleprice) * convertsaleprice).ToString();
                                }
                            }
                            else if (convertsaleprice > 1)
                            {
                                if (dish.Saleprice == null)
                                {
                                    dish.Saleprice = (dish.price - convertsaleprice).ToString();
                                }
                                else
                                {
                                    dish.Saleprice = (int.Parse(dish.Saleprice) - convertsaleprice).ToString();
                                }
                            }
                        }
                    }
                }
            }

            List<DishModel> dishDiscounts = new List<DishModel>();
            dishDiscounts = discountDAO.ListDishDiscount();
            dishDiscounts = DeleteduplicatesDish(dishDiscounts);
            foreach (var dish in DishsLocal)
            {
                foreach (var dishDiscount in dishDiscounts)
                {
                    if (dish._id == dishDiscount._id)
                    {
                        List<string> pricedishdiscounts = new List<string>();
                        dish.SaleDish = true;
                        pricedishdiscounts = discountDAO.MutiPriceSaleWithDish(dish._id);
                        pricedishdiscounts = Drop_MoneyToPercent(pricedishdiscounts);
                        foreach (var pdc in pricedishdiscounts)
                        {
                            double convertsaleprice = Double.Parse(pdc);
                            if (0 < convertsaleprice && convertsaleprice <= 1)
                            {
                                if (dish.Saleprice == null)
                                {
                                    dish.Saleprice = (dish.price - dish.price * convertsaleprice).ToString();
                                }
                                else
                                {
                                    dish.Saleprice = (int.Parse(dish.Saleprice) - int.Parse(dish.Saleprice) * convertsaleprice).ToString();
                                }
                            }
                            else if (convertsaleprice > 1)
                            {
                                if (dish.Saleprice == null)
                                {
                                    dish.Saleprice = (dish.price - convertsaleprice).ToString();
                                }
                                else
                                {
                                    dish.Saleprice = (int.Parse(dish.Saleprice) - convertsaleprice).ToString();
                                }
                            }
                        }
                    }
                }
            }
            return DishsLocal;
        }

        public List<DishModel> LoadDishWithType(List<DishModel> AllDishsVariable, string Type) // 
        {
            List<DishModel> DishsLocal = new List<DishModel>();
            if (Type == "All Dishs")
            {
                return AllDishsVariable;
            }
            else
            {
                if (Type != null)
                {
                    foreach (var dish in AllDishsVariable)
                    {
                        if (dish.category == Type)
                        {
                            DishsLocal.Add(dish);
                        }
                    }
                    return DishsLocal;
                }
            }
            return DishsLocal;
        }

        public List<DishModel> LoadOnlydiscountfortype(List<DishModel> AllDishsVariable, string Type_Special, string Type)// cho qua
        {
            List<DishModel> OnlyDiscount = new List<DishModel>();
            if (AllDishsVariable != null)
            {
                if (Type_Special == "Discounted")
                {
                    foreach (var dish in AllDishsVariable)
                    {
                        if (dish.SaleDish == true)
                        {
                            OnlyDiscount.Add(dish);
                        }
                    }
                    return OnlyDiscount;
                }
                else if (Type_Special == "Out Spectial")
                {
                   return LoadDishWithType(AllDishsVariable, Type);
                }
            }
            return OnlyDiscount;
        }

        public List<DishModel> filter(List<DishModel> AllDishsVariable, string types_dish_search, string Type, string Type_Special)
        {
            List<DishModel> Dishs_Search = new List<DishModel> ();
            if (types_dish_search != "")
            {
                foreach (var item in AllDishsVariable)
                {
                    bool contains = item.dish_name.Contains(types_dish_search);
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
                        if(AllDishsVariable.IndexOf(item) == AllDishsVariable.Count)
                        {
                            if(Dishs_Search.Count == 0)
                            {
                                return null;
                            }
                        }
                    }
                }
                if (Dishs_Search.Count > 0)
                {
                    return Dishs_Search;
                }
            }
            else
            {
                if (Type != "All Dishs")
                {
                    
                    if (Type_Special == "Discounted")
                    {
                        LoadOnlydiscountfortype(AllDishsVariable,Type_Special,Type);
                    }
                }
                else
                {
                    if (Type_Special == "Discounted")
                    {
                        LoadOnlydiscountfortype(AllDishsVariable, Type_Special, Type);
                    }
                }
                return LoadOnlydiscountfortype(AllDishsVariable, Type_Special, Type);
            }
            return Dishs_Search;
        }

        private List<string> Drop_MoneyToPercent(List<string> listdiscount)
        {
            var sortedList = listdiscount.OrderByDescending(item => item).ToList();
            return sortedList;
        }
        private List<MenuModel> DeleteduplicatesMenu(List<MenuModel> menudiscount)
        {
            List<MenuModel> uniqueItems = menudiscount
            .GroupBy(item => item.id)
            .Select(group => group.First())
            .ToList();

            return uniqueItems;
        }

        private List<DishModel> DeleteduplicatesDish(List<DishModel> dishdiscount)
        {
            List<DishModel> uniqueItems = dishdiscount
            .GroupBy(item => item._id)
            .Select(group => group.First())
            .ToList();

            return uniqueItems;
        }
    }
}
