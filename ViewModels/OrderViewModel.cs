﻿using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevCoffeeManagerApp.DAOs;
using System.Windows.Input;
using MongoDB.Driver;
using DevCoffeeManagerApp.Commands.CommandOrder;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        MenuDAO menuDao = new MenuDAO();
        DiscountDAO discountDAO = new DiscountDAO();
        public List<DishModel> AllDishs; 
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
        private string _search = "";
        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
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
        public List<string> Tags { get; set; }
        private string _type_special = "Remove Tag";
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
        public List<String> Ice { get; set; }
        public List<String> Sugar { get; set; }

        public ICommand MinusCommad { get; set; }
        public ICommand PlusCommad { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand Deleteall { get; set; }
        public ICommand ReserveCommand { get; set; }
        public ICommand UpdateSugarCommand { get; set; }
        public ICommand OrderFoodCommand { get; set; }
        public ICommand SelectionchangeTypeDish { get; set; }
        public ICommand ChangeValueTexboxCommand { get; set; }
        public ICommand SelectionchangeTypeSpecial { get; set; }
        public OrderViewModel(NavigationStore navigationStore)
        {
            Ordereds = new ObservableCollection<DishModel>();
            types_dish = load_types_dish();
            Tags = load_Tag();

            if(SessionStatic.Dishs == null)
            {
                SessionStatic.Dishs = LoadAllDish();
                AllDishs = SessionStatic.Dishs;
                Dishs = AllDishs;
            }
            else
            {
                AllDishs = SessionStatic.Dishs;
                Dishs = AllDishs;
            }

            add_percent();
            PlusCommad = new OperatorCommand(this, "Plus");
            MinusCommad = new OperatorCommand(this, "Minus");
            DeleteCommand = new OperatorCommand(this, "Delete");
            Deleteall = new OperatorCommand(this, "DeleteAll");
            ReserveCommand = new AddDishCommand(this);
            UpdateSugarCommand = new UpdateOrderCommand(this);

            SelectionchangeTypeDish = new SearchCommand(this);
            ChangeValueTexboxCommand = new SearchCommand(this);
            SelectionchangeTypeSpecial = new SearchCommand(this);
            OrderFoodCommand = new OrderConfirmCommand(this, navigationStore);

            if (SessionStatic.GetOrdereds != null)
            {
                Ordereds = SessionStatic.DeepCopyObservableCollection(SessionStatic.GetOrdereds);
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

        public List<string> load_Tag()
        {
            List<string> Tags = new List<string>();
            Tags.Add("Remove Tag");
            Tags.Add("New Dish");
            Tags.Add("Hot Dish");
            Tags.Add("Discounted");

            return Tags;
        }
        public void add_percent()
        {
            Ice = new List<string>();
            Ice.Add("No Ice");
            Ice.Add("30%");
            Ice.Add("70%");
            Ice.Add("100%");

            Sugar = new List<string>();
            Sugar.Add("No Sugar");
            Sugar.Add("30%");
            Sugar.Add("70%");
            Sugar.Add("100%");
        }

        public List<DishModel> LoadAllDish()
        {
            // Add menu tag
            List<DishModel> DishsLocal = new List<DishModel>();
            foreach (var type in load_types_dish())
            {
                List<DishModel> DishsTemp = new List<DishModel>();
                if (type != "All Dishs")
                {
                    DishsTemp = menuDao.ReadOnetype(type).dish;
                    foreach (var dishtemp in DishsTemp)
                    {
                        dishtemp.category = type;
                    }
                }
                DishsLocal.AddRange(DishsTemp);
            }
            // Add menu discount
            List<MenuModel> DiscountMenu = new List<MenuModel>();
            DiscountMenu = discountDAO.ListMenuDiscount();
            if(DiscountMenu != null)
            {
                // 1 menu - n discount
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
                            listpricediscountmenu = Sort_Discount(listpricediscountmenu);
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
            }
            // Add dish discount
            List<DishModel> dishDiscounts = new List<DishModel>();
            dishDiscounts = discountDAO.ListDishDiscount();
            if(dishDiscounts != null)
            {
                // 1 dish - n discount
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
                            pricedishdiscounts = Sort_Discount(pricedishdiscounts);
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
            }
            
            // Add "new" tag
            List<DishModel> DishsNew = new List<DishModel>();
            DishsNew = menuDao.ReadAll_NewDish();
            for (int i = 0; i < DishsLocal.Count(); i++)
            {
                for (int j = 0; j < DishsNew.Count(); j++)
                {
                    if (DishsLocal[i]._id == DishsNew[j]._id)
                    {
                        DishsLocal[i].newDish = true;
                    }
                }
            }
            // Add "hot" tag
            List<DishModel> DishsHot = new List<DishModel>();
            DishsHot = menuDao.ReadAll_HotDish();
            for (int i = 0; i < DishsLocal.Count(); i++)
            {
                for (int j = 0; j < DishsHot.Count(); j++)
                {
                    if (DishsLocal[i]._id == DishsHot[j]._id)
                    {
                        DishsLocal[i].HotDish = true;
                    }
                }
            }

            return DishsLocal;
        }

        private List<string> Sort_Discount(List<string> listdiscount)
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
