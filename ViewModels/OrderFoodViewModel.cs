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

        public List<string> types_dish
        {
            get; set;
        }
        private string _type;
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
                Dishs = menuDao.ReadAll(Type).dish;
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
        public ICommand MinusCommad { get; set; }
        public ICommand PlusCommad { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand ReserveCommand { get; set; }
        public OrderFoodViewModel()
        {
            Ordereds = new ObservableCollection<StructOfOrderedItem>();
            List<DishModel> dishcombined = new List<DishModel>();
            ObservableCollection<MenuModel> menuModels = menuDao.ReadAll_Type_dish();
            List<string> temp = new List<string>();
            foreach (var item in menuModels)
            {
                List<DishModel> dish_in_type = new List<DishModel>();
                dish_in_type = menuDao.ReadAll(item.type_of_dish).dish;
                dishcombined.AddRange(dish_in_type);
                temp.Add(item.type_of_dish);
            }
            types_dish = temp;
            Dishs = dishcombined;
            PlusCommad = new Add_Munix_Delete_Command(this, "Plus");
            MinusCommad = new Add_Munix_Delete_Command(this, "Minus");
            DeleteCommand = new Add_Munix_Delete_Command(this, "Delete");

            ReserveCommand = new ReserveCommand(this);
        }

    }

}
