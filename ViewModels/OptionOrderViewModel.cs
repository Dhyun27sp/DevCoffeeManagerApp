using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.StaticClass;
using System.Windows.Input;
using DevCoffeeManagerApp.Commands.CommadOrders;
using DevCoffeeManagerApp.Commands.CommandSell;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OptionOrderViewModel : BaseViewModel
    {
        private ObservableCollection<DishModel> _orderedFood;
        public ObservableCollection<DishModel> OrderedFood
        {
            get 
            { 
                return _orderedFood; 
            }
            set 
            { 
                _orderedFood = value;
                OnPropertyChanged(nameof(OrderedFood));
                int index = 1;
                foreach (var O in OrderedFood)
                {
                    O.Amount = (int.Parse(O.Quantity) * int.Parse(O.Saleprice)).ToString();
                    O.OrdinalNumber = index;
                    index++;
                }
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
        public OptionOrderViewModel() 
        { 
            if(SessionStatic.Ordereds != null)
            {
                int index = 1;
                OrderedFood = SessionStatic.Ordereds;
                foreach(var O in OrderedFood) {
                    O.Amount = (int.Parse(O.Quantity) * int.Parse(O.Saleprice)).ToString();
                    O.OrdinalNumber = index;
                    index++;
                }
            }
            PlusCommad = new Add_Munix_Delete_in_Option_Command(this, "Plus");
            MinusCommad = new Add_Munix_Delete_in_Option_Command(this, "Minus");
            DeleteCommand = new Add_Munix_Delete_in_Option_Command(this, "Delete");
        }
    }
}
