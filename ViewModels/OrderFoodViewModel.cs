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
using DevCoffeeManagerApp.Component.ComPonentOrder;
using DevCoffeeManagerApp.Commands.CommadOrders;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OrderFoodViewModel : BaseViewModel
    {
        MenuDAO menuDao = new MenuDAO();
        DiscountDAO discountDAO = new DiscountDAO();
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
        private string _types_dish_search;
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
                if (types_dish_search !="")
                {
                    LoadDishWithType();
                    foreach (var item in Dishs)
                    {
                        bool contains = item.dish_name.Contains(types_dish_search);
                        if (contains)
                        {
                            Dishs_Search.Add(item);
                        }
                        else
                        {
                            LoadDishWithType();
                        }
                    }
                    if (Dishs_Search.Count > 0)
                    {
                        Dishs = Dishs_Search;
                    }
                }
                else
                {
                    LoadDishWithType();
                }
                LoadMenuDiscount();
                LoadDishDistcount();
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
                LoadDishWithType();
                LoadMenuDiscount();
                LoadOnlydiscountfortype();
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

        private ObservableCollection<StructOfOrderedItem> _ordereds;
        public ObservableCollection<StructOfOrderedItem> Ordereds
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
                LoadMenuDiscount();
                LoadOnlydiscountfortype();
            }
        }

        public ICommand MinusCommad { get; set; }
        public ICommand PlusCommad { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand Deleteall { get; set; }
        public ICommand ReserveCommand { get; set; }
        public OrderFoodViewModel()
        {
            Ordereds = new ObservableCollection<StructOfOrderedItem>();
            Specials = new List<string>();
            ObservableCollection<MenuModel> menuModels = menuDao.ReadAll_Type_dish();
            List<string> temp = new List<string>();
            temp.Add("All Dishs");
            foreach (var item in menuModels)
            {
                List<DishModel> dish_in_type = new List<DishModel>();
                temp.Add(item.type_of_dish);
            }

            Specials.Add("Out Spectial");
            Specials.Add("New Dish");
            Specials.Add("Hot Dish");
            Specials.Add("Discounted");

            types_dish = temp;
            Dishs = menuDao.ReadAll_Dish();
            LoadMenuDiscount();
            LoadDishDistcount();
            PlusCommad = new Add_Munix_Delete_Command(this, "Plus");
            MinusCommad = new Add_Munix_Delete_Command(this, "Minus");
            DeleteCommand = new Add_Munix_Delete_Command(this, "Delete");
            Deleteall = new Add_Munix_Delete_Command(this, "DeleteAll");
            ReserveCommand = new ReserveCommand(this);
        }

        private void LoadDishWithType()
        {
            if (Type == "All Dishs")
            {
                Dishs = menuDao.ReadAll_Dish();
                LoadDishDistcount();
            }
            else
            {
                if (Type != null)
                {
                    Dishs = menuDao.ReadAll(Type).dish;
                    LoadDishDistcount();
                }
            }
        }

        private void LoadDishDistcount()
        {
            List<DishModel> dishDiscounts = new List<DishModel>();
            dishDiscounts = discountDAO.ListDishDiscount();
            foreach (var dish in Dishs)
            {
                foreach (var dishDiscount in dishDiscounts)
                {
                    if (dish._id == dishDiscount._id)
                    {
                        dish.SaleDish = "Sale";
                        dish.Hidden = "Visible";
                    }
                }
            }
        }
        private void LoadOnlydiscountfortype()
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
            else if(Type_Special ==  "Out Spectial")
            {
                LoadDishWithType();
                LoadMenuDiscount();
            }
        }

        private void LoadMenuDiscount()
        {
            List<MenuModel> DiscountMenu = new List<MenuModel>();
            List<DishModel> DishInType = new List<DishModel>();
            DiscountMenu = discountDAO.ListMenuDiscount();
            foreach(var menu in DiscountMenu)
            {
                foreach(var type in types_dish)
                {
                    if(menu.type_of_dish == type)
                    {
                        foreach(var dish in Dishs)
                        {
                            DishInType = menuDao.ReadAll(type).dish;
                            foreach (var dishsale in DishInType)
                            {
                                if(dish._id == dishsale._id)
                                {
                                    dish.SaleDish = "Sale";
                                    dish.Hidden = "Visible";
                                }
                            }
                        }
                    }
                }
            }
        }

    }

}
