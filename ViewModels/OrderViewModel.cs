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
using DevCoffeeManagerApp.Commands.CommandOrder;
using DevCoffeeManagerApp.Commands.CommandOption;
using DevCoffeeManagerApp.Commands.CommandStaff;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.Views.UserControlStaff;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OrderViewModel : BaseViewModel
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
            }
        }
        public List<string> Specials { get; set; }
        private string _type_special = "Out Spectial";
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
        
        private int _total = 0;

        public int Total
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
        public ICommand OrderFoodCommand { get; set; }
        public ICommand SelectionchangeTypeDish { get; set; }
        public ICommand ChangeValueTexboxCommand { get; set; }
        public ICommand SelectionchangeTypeSpecial { get; set; }
        public OrderViewModel(NavigationStore navigationStore)
        {
            Ordereds = new ObservableCollection<DishModel>();
            types_dish = load_types_dish();
            Specials = loadSpecials();
            if(SessionStatic.Dishs == null)
            {
                SessionStatic.Dishs = LoadAllDish();
                AllDishsVariable = SessionStatic.Dishs;
                Dishs = AllDishsVariable;
            }
            else
            {
                AllDishsVariable = SessionStatic.Dishs;
                Dishs = AllDishsVariable;
            }
            PlusCommad = new OperatorCommand(this, "Plus");
            MinusCommad = new OperatorCommand(this, "Minus");
            DeleteCommand = new OperatorCommand(this, "Delete");
            Deleteall = new OperatorCommand(this, "DeleteAll");
            ReserveCommand = new AddDishCommand(this);
            SelectionchangeTypeDish = new FilterByTypeCommand(this);
            ChangeValueTexboxCommand = new SearchDishCommand(this);
            SelectionchangeTypeSpecial = new FilterBySpecialCommand(this);
            OrderFoodCommand = new OrderConfirmCommand(this, navigationStore);
            if (SessionStatic.Ordereds != null)
            {
                Ordereds = SessionStatic.Ordereds;
                foreach (var O in Ordereds)
                {
                    if (O.Saleprice != null)
                    {
                        Total += O.Quantity * (int)O.Saleprice;
                    }
                    else
                    {
                        Total += O.Quantity * (int)O.price;
                    }
                }
            }
        }

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
            List<string> Specials = new List<string>();
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
                                    dish.Saleprice = (int)(dish.price - dish.price * convertsaleprice);
                                }
                                else
                                {
                                    dish.Saleprice -= (int)(dish.price * convertsaleprice);
                                }
                            }
                            else if (convertsaleprice > 1)
                            {
                                if (dish.Saleprice == null)
                                {
                                    dish.Saleprice = (int)(dish.price - convertsaleprice);
                                }
                                else
                                {
                                    dish.Saleprice -= (int)convertsaleprice;
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
                                    dish.Saleprice = (int)(dish.price - dish.price * convertsaleprice);
                                }
                                else
                                {
                                    dish.Saleprice -= (int)(dish.price * convertsaleprice);
                                }
                            }
                            else if (convertsaleprice > 1)
                            {
                                if (dish.Saleprice == null)
                                {
                                    dish.Saleprice = (int)(dish.price - convertsaleprice);
                                }
                                else
                                {
                                    dish.Saleprice -= (int)(convertsaleprice);
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

        public List<DishModel> LoadOnlydiscountfortype(List<DishModel> AllDishsVariable, string Type_Special, string Type)
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
