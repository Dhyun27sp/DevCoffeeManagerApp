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
using DevCoffeeManagerApp.Commands.CommandMainStaff;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.Services.ServiceOrder;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OrderFoodViewModel : BaseViewModel
    {
        OrderServiceImpl OrderServiceImpl = new OrderServiceImpl();

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
                Dishs = OrderServiceImpl.filter(AllDishsVariable, types_dish_search, Type, Type_Special);
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
                Dishs = OrderServiceImpl.filter(AllDishsVariable, types_dish_search, Type, Type_Special);
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
                Dishs = OrderServiceImpl.filter(AllDishsVariable, types_dish_search, Type, Type_Special);
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
        public ICommand OrderFoodCommand { get; set; }
        public OrderFoodViewModel(NavigationStore navigationStore)
        {
            Ordereds = new ObservableCollection<DishModel>();
            types_dish = OrderServiceImpl.load_types_dish();
            Specials = OrderServiceImpl.loadSpecials();
            AllDishsVariable = OrderServiceImpl.LoadAllDish();
            Dishs = AllDishsVariable;

            PlusCommad = new Add_Munix_Delete_Command(this, "Plus");
            MinusCommad = new Add_Munix_Delete_Command(this, "Minus");
            DeleteCommand = new Add_Munix_Delete_Command(this, "Delete");
            Deleteall = new Add_Munix_Delete_Command(this, "DeleteAll");
            ReserveCommand = new ReserveCommand(this);
            OrderFoodCommand = new OrderConfirmationCommand(this, navigationStore);
            if (SessionStatic.Ordereds != null)
            {
                Ordereds = SessionStatic.Ordereds;
            }
        }
    }

}
