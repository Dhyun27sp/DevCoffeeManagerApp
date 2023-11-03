using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Component.ComPonentOrder
{
    public class StructOfOrderedItem : BaseViewModel
    {
        private string _name_dish;

        public string Name_Dish
        {
            get
            {
                return _name_dish;
            }

            set
            {
                _name_dish = value;
                OnPropertyChanged(nameof(Name_Dish));
            }
        }

        private string _quantity;

        public string Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }


        public StructOfOrderedItem(string _name_dish, string _quantity)
        {
            this._quantity = _quantity;
            this._name_dish = _name_dish;
        }
    }
}
