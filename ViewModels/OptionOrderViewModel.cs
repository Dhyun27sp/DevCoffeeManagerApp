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
using DevCoffeeManagerApp.Commands.CommandOptionOrder;

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
                    if(O.Saleprice != null)
                    {
                        O.Amount = (int.Parse(O.Quantity) * int.Parse(O.Saleprice)).ToString();
                    }
                    else
                    {
                        O.Amount = (int.Parse(O.Quantity) * O.price).ToString();
                    }
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

        private double _plus_point;
        public double PlusPoint
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
        public ICommand MinusCommad { get; set; }
        public ICommand PlusCommad { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCustomerCommand { get; set; }
        public OptionOrderViewModel() 
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
                    if (O.Saleprice != null)
                    {
                        Total = (Int32.Parse(Total) + (int.Parse(O.Quantity) * int.Parse(O.Saleprice))).ToString();
                    }
                    else
                    {
                        Total = (Int32.Parse(Total) + (int.Parse(O.Quantity) * O.price)).ToString();
                    }
                }
            }
            if (SessionStatic.GetTables != null)
            {
                foreach (var table in SessionStatic.GetTables)
                {
                    if(table.No_ >= 1 && table.No_ <= 4)
                    {
                        table.Floor = 1;
                    }
                    else if (table.No_ >= 5 && table.No_ <= 10)
                    {
                        table.Floor = 2;
                    }
                }
                ObservableCollection<TableModel> SortBookTable = new ObservableCollection<TableModel>();
                SortBookTable = FloorSort(SessionStatic.GetTables);
                BookedTable = SortBookTable;
            }
            PlusCommad = new Add_Munix_Delete_in_Option_Command(this, "Plus");
            MinusCommad = new Add_Munix_Delete_in_Option_Command(this, "Minus");
            DeleteCommand = new Add_Munix_Delete_in_Option_Command(this, "Delete");
            SearchCustomerCommand = new SearchCustomerCommand(this);
            PlusPoint = Int32.Parse(Total) * 0.01;
        }

        private ObservableCollection<TableModel> FloorSort(ObservableCollection<TableModel> BookedTableOrigin) //Sắp xếp tầng
        {
            ObservableCollection<TableModel> SortBookTable = new ObservableCollection<TableModel>();
            SortBookTable = BookedTableOrigin;
            // Điều kiện lọc
            Func<TableModel, bool> condition = item => item.Floor==1;

            // Lọc và sắp xếp
            var filteredItems = SortBookTable.Where(condition).ToList();
            var otherItems = SortBookTable.Except(filteredItems).ToList();

            // Ghép các phần tử thỏa mãn điều kiện lên đầu
            SortBookTable.Clear();
            foreach (var item in filteredItems.Concat(otherItems))
            {
                SortBookTable.Add(item);
            }
            return SortBookTable;
        }
    }
}
