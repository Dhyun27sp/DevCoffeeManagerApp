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
        public ICommand testchageitem { get; set; }

        private string _type_dish = "Coffee";
        public string Type_dish
        {
            get
            {
                return _type_dish;
            }
            set
            {
                _type_dish = value;
                OnPropertyChanged(nameof(Type_dish));
            }
        }

        public OrderFoodViewModel()
        {
            Dishs = menuDao.ReadAll(Type_dish).dish;
        }
    }
}
