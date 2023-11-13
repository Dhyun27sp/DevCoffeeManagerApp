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
                int index = 1;
                foreach (var O in OrderedFood)
                {
                    O.Amount = (int.Parse(O.Quantity) * int.Parse(O.Saleprice)).ToString();
                    CombineList.Add(new Tuple<DishModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(OrderedFood));
            }
        }
        private ObservableCollection<int> _indexList = new ObservableCollection<int>();
        public ObservableCollection<int> IndexList
        {
            get
            {
                return _indexList;
            }

            set
            {
                _indexList = value;
                OnPropertyChanged(nameof(IndexList));
            }
        }
        private ObservableCollection<Tuple<DishModel, int>> _combineList = new ObservableCollection<Tuple<DishModel, int>>();
        public ObservableCollection<Tuple<DishModel, int>> CombineList
        {
            get
            {
                return _combineList;
            }
            set
            {   
                OnPropertyChanged(nameof(CombineList));
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
            if (SessionStatic.Ordereds != null)
            {
                OrderedFood = SessionStatic.Ordereds;
            }
            PlusCommad = new Add_Munix_Delete_in_Option_Command(this, "Plus");
            MinusCommad = new Add_Munix_Delete_in_Option_Command(this, "Minus");
            DeleteCommand = new Add_Munix_Delete_in_Option_Command(this, "Delete");
        }
    }
}
