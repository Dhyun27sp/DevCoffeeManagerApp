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

        public OrderFoodViewModel()
        {
            List<MenuModel> menuModels = menuDao.ReadAll_Type_dish();
            List<string> temp = new List<string>();
            foreach (var item in menuModels)
            {
                temp.Add(item.type_of_dish);
            }
            types_dish = temp;          
        }
    }
}
