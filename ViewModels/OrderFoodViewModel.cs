using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using DevCoffeeManagerApp.DAOs;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DevCoffeeManagerApp.Commands.CommadOrders;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OrderFoodViewModel : BaseViewModel
    {
        MenuDAO menuDao = new MenuDAO();
        DiscountDAO discountDAO = new DiscountDAO();
        public List<DishModel> AllDishsVariable;
        private List<string> _types_dish ;
        public List<string> types_dish
        {
            get 
            {
                return _types_dish;
            }
            set
            {
                _types_dish = value;
                OnPropertyChanged(nameof(types_dish));
            }
        }
        private string _types_dish_search = "";
        public string types_dish_search
        {
            get
            {
                return _types_dish_search;
            }
            set
            {
                _types_dish_search = value;
                OnPropertyChanged(nameof(types_dish_search));
                List<DishModel> Dishs_Search = new List<DishModel>();
                filter(Dishs_Search);
            }
        }
        private string _type = "All Dishs";
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
                types_dish_search = types_dish_search;// lọc lại 
            }
        }
        private List<DishModel> _dish;
        public List<DishModel> Dishs
        {
            get
            {
                return _dish;
            }
            set
            {
                _dish = value;
                OnPropertyChanged(nameof(Dishs));
            }
        }

        private ObservableCollection<DishModel> _ordereds;
        public ObservableCollection<DishModel> Ordereds
        {
            get
            {
                return _ordereds;
            }
            set
            {
                _ordereds = value;
                OnPropertyChanged(nameof(Ordereds));
            }
        }
        public List<string> Specials { get; set; }
        private string _type_special;
        public string Type_Special
        {
            get
            {
                return _type_special;
            }
            set
            {
                _type_special = value;
                OnPropertyChanged(nameof(Type_Special));
                types_dish_search = types_dish_search;// lọc lại 
            }
        }
        private string _total = "0";

        public string Total
        {
            get
            {
                return _total;
            }

            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public ICommand MinusCommad { get; set; }
        public ICommand PlusCommad { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand Deleteall { get; set; }
        public ICommand ReserveCommand { get; set; }
        public OrderFoodViewModel()
        {
            Ordereds = new ObservableCollection<DishModel>();
            load_types_dish();
            loadSpecials();
            Dishs = LoadAllDish();
            AllDishsVariable = LoadAllDish();

            PlusCommad = new Add_Munix_Delete_Command(this, "Plus");
            MinusCommad = new Add_Munix_Delete_Command(this, "Minus");
            DeleteCommand = new Add_Munix_Delete_Command(this, "Delete");
            Deleteall = new Add_Munix_Delete_Command(this, "DeleteAll");
            ReserveCommand = new ReserveCommand(this);
        }
        private void load_types_dish()
        {
            ObservableCollection<MenuModel> menuModels = menuDao.ReadAll_Type_dish();
            List<string> temp = new List<string>();
            temp.Add("All Dishs");
            foreach (var item in menuModels)
            {
                List<DishModel> dish_in_type = new List<DishModel>();
                temp.Add(item.type_of_dish);
            }
            types_dish = temp;
        }
        private void loadSpecials()
        {
            Specials = new List<string>();
            Specials.Add("Out Spectial");
            Specials.Add("New Dish");
            Specials.Add("Hot Dish");
            Specials.Add("Discounted");
        }
        private List<DishModel> LoadAllDish()
        {
            List<DishModel> DishsLocal = new List<DishModel>();
            foreach (var type in types_dish)
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
            DiscountMenu = removegermsMenudiscount(DiscountMenu);
            foreach (var menu in DiscountMenu)
            {
                foreach (var dish in DishsLocal)
                {
                    if (dish.category == menu.type_of_dish)
                    {
                        List<string> listpricediscountmenu = new List<string>();
                        dish.SaleDish = "Sale";
                        dish.Hidden = "Visible";
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
                                if(dish.Saleprice == null)
                                {
                                    dish.Saleprice = (dish.price - convertsaleprice).ToString();
                                }
                                else
                                {
                                    dish.Saleprice = (int.Parse(dish.Saleprice) - convertsaleprice).ToString();
                                }
                            }
                        }
                        dish.Strikethrough = "Strikethrough";
                    }
                }
            }

            List<DishModel> dishDiscounts = new List<DishModel>();
            dishDiscounts = discountDAO.ListDishDiscount();
            dishDiscounts = removegermsDishdiscount(dishDiscounts);
            foreach (var dish in DishsLocal)
            {
                foreach (var dishDiscount in dishDiscounts)
                {
                    if (dish._id == dishDiscount._id)
                    {
                        List<string> pricedishdiscounts = new List<string>();
                        dish.SaleDish = "Sale";
                        dish.Hidden = "Visible";
                        pricedishdiscounts = discountDAO.MutiPriceSaleWithDish(dish._id);
                        pricedishdiscounts = Drop_MoneyToPercent(pricedishdiscounts);
                        foreach (var pdc in pricedishdiscounts)
                        {
                            double convertsaleprice = Double.Parse(pdc);
                            if (0 < convertsaleprice && convertsaleprice <= 1)
                            {
                               if(dish.Saleprice == null)
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
                        dish.Strikethrough = "Strikethrough";
                    }
                }
            }
            return DishsLocal;
        }
        private void LoadDishWithType()
        {
            List<DishModel> DishsLocal = new List<DishModel>();
            if (Type == "All Dishs")
            {
                Dishs = AllDishsVariable;
            }
            else
            {
                if (Type != null)
                {
                    Dishs = AllDishsVariable;
                    foreach (var dish in Dishs)
                    {
                        if(dish.category == Type)
                        {
                            DishsLocal.Add(dish);
                        }
                    }
                    Dishs = DishsLocal;
                }
            }
        }
        private void LoadOnlydiscountfortype()
        {
            if(Dishs != null)
            {
                List<DishModel> OnlyDiscount = new List<DishModel>();
                if (Type_Special == "Discounted")
                {
                    foreach (var dish in Dishs)
                    {
                        if (dish.SaleDish == "Sale")
                        {
                            OnlyDiscount.Add(dish);
                        }
                    }
                    Dishs = OnlyDiscount;
                }
                else if (Type_Special == "Out Spectial")
                {
                    LoadDishWithType();
                }
            }
        }

        private void filter(List<DishModel> Dishs_Search)
        {
            if (types_dish_search != "")
            {
                Dishs = AllDishsVariable;
                foreach (var item in Dishs)
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
                                    if (item.SaleDish == "Sale")
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
                                if (item.SaleDish == "Sale")
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
                        Dishs = null;
                    }
                }
                if (Dishs_Search.Count > 0)
                {
                    Dishs = Dishs_Search;
                }
            }
            else
            {
                if (Type != "All Dishs")
                {
                    LoadDishWithType();
                    if (Type_Special == "Discounted")
                    {
                        LoadOnlydiscountfortype();
                    }
                }
                else
                {
                    Dishs = AllDishsVariable;
                    if (Type_Special == "Discounted")
                    {
                        LoadOnlydiscountfortype();
                    }
                }
            }
        }
        
        private List<string> Drop_MoneyToPercent(List<string> listdiscount)
        {
            var sortedList = listdiscount.OrderByDescending(item => item).ToList();
            return sortedList;
        }
        private List<MenuModel> removegermsMenudiscount(List<MenuModel> menudiscount)
        {
            List<MenuModel> uniqueItems = menudiscount
            .GroupBy(item => item.id)
            .Select(group => group.First())
            .ToList();

            return uniqueItems;
        }

        private List<DishModel> removegermsDishdiscount(List<DishModel> dishdiscount)
        {
            List<DishModel> uniqueItems = dishdiscount
            .GroupBy(item => item._id)
            .Select(group => group.First())
            .ToList();

            return uniqueItems;
        }
    }

}
