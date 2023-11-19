using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.StaticClass;
using System.Windows.Input;
using DevCoffeeManagerApp.Commands.CommandOrder;
using DevCoffeeManagerApp.Commands.CommandSell;
using DevCoffeeManagerApp.Commands.CommandOption;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OptionViewModel : BaseViewModel
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
                CombineList.Clear();
                foreach (var O in OrderedFood)
                {
                    if(O.Saleprice != null)
                    {
                        O.Amount = O.Quantity * (int)O.Saleprice;
                    }
                    else
                    {
                        O.Amount = O.Quantity * (int)O.price;
                    }
                    CombineList.Add(new Tuple<DishModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(OrderedFood));
                
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
                _combineList = value;
                OnPropertyChanged(nameof(CombineList));
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

        private int _plus_point = 0;
        public int PlusPoint
        {
            get 
            { 
                return _plus_point; 
            }
            set 
            { 
                _plus_point = value; 
                OnPropertyChanged(nameof(PlusPoint)); 
            }
        }

        private int _point;
        public int Point
        {
            get
            {
                return _point;
            }
            set
            {
                _point = value;
                OnPropertyChanged(nameof(Point));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private ObservableCollection<TableModel> _bookedTable;
        public ObservableCollection<TableModel> BookedTable
        {
            get
            {
                return _bookedTable;
            }
            set
            {
                _bookedTable = value;
                OnPropertyChanged(nameof(BookedTable));
            }
        }


        private int _usePointText = 0;
        public int UsePointText
        {
            get
            {
                return _usePointText;
            }
            set
            {
                _usePointText = value;
                OnPropertyChanged(nameof(UsePointText));
            }
        }

        public ICommand MinusCommad { get; set; }
        public ICommand PlusCommad { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCustomerCommand { get; set; }
        public ICommand UsePointCommand { get; set; }
        public OptionViewModel() 
        {
            if(SessionStatic.Customer != null)
            {
                PhoneNumber = SessionStatic.Customer.phone_number;
                Point = SessionStatic.Customer.point;
            }
            if (SessionStatic.Ordereds != null)
            {
                OrderedFood = SessionStatic.Ordereds;
                foreach (var O in OrderedFood)
                {
                    Total = Total + O.Amount;
                    
                }
                PlusPoint = (int)(Total * 0.01);
            }
            if (SessionStatic.GetTables != null)
            {
                BookedTable = TableSort();
            }
            
            PlusCommad = new OperatorCommand(this, "Plus");
            MinusCommad = new OperatorCommand(this, "Minus");
            DeleteCommand = new OperatorCommand(this, "Delete");
            SearchCustomerCommand = new SearchCustomerCommand(this);      
            UsePointCommand = new UsePointCommand(this);
        }

        private ObservableCollection<TableModel> TableSort() //Sắp xếp thứ tự đã đặt các bàn theo id bàn
        {
            ObservableCollection<TableModel> SortBookTable = new ObservableCollection<TableModel>();
            foreach (var table in SessionStatic.GetTables)
            {
                if (table.No_ >= 1 && table.No_ <= 4)
                {
                    table.Floor = 1;
                }
                else if (table.No_ >= 5 && table.No_ <= 10)
                {
                    table.Floor = 2;
                }
            }
            SortBookTable = new ObservableCollection<TableModel>(SessionStatic.GetTables.OrderBy(table => table.No_));
            return SortBookTable;
        }
    }
}
