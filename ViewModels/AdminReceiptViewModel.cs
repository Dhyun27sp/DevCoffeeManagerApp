using DevCoffeeManagerApp.Commands.AdminCommand.CustomerCommands;
using DevCoffeeManagerApp.Commands.AdminCommand.ReceiptCommands;
using DevCoffeeManagerApp.Commands.CommandAdminReceipt;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminReceiptViewModel : BaseViewModel
    {
        ReceiptDAO receiptDAO = new ReceiptDAO();

        private ObservableCollection<ReceiptModel> _receipt;
        public ObservableCollection<ReceiptModel> Receipts
        {
            get
            {
                return _receipt;
            }
            set
            {
                _receipt = value;
                int index = 1;
                CombineList.Clear();
                foreach (var O in Receipts)
                {
                    string tables = "";
                    if (O.tables != null)
                    {
                        foreach (var i in O.tables)
                        {
                            if (tables == "")
                            {
                                tables = i.No_.ToString();
                            }
                            else
                                tables = tables + ", " + i.No_.ToString();
                        }
                    }
                    CombineList.Add(new Tuple<ReceiptModel, int, string>(O, index, tables));
                    index++;
                }
                OnPropertyChanged(nameof(Receipts));

            }
        }
        private ObservableCollection<Tuple<ReceiptModel, int, string>> _combineList = new ObservableCollection<Tuple<ReceiptModel, int, string>>();
        public ObservableCollection<Tuple<ReceiptModel, int, string>> CombineList
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
        private string _payment;
        public string Payment
        {
            get { return _payment; }
            set
            {
                _payment = value;
                OnPropertyChanged(nameof(Payment));
            }
        }

        private DateTime _currentDate;
        public DateTime CurrentDate
        {
            get
            {
                return _currentDate;
            }

            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }


        private string _staffPhoneNumber;
        public string StaffPhoneNumber
        {
            get
            {
                return _staffPhoneNumber;
            }

            set
            {
                _staffPhoneNumber = value;
                OnPropertyChanged(nameof(StaffPhoneNumber));
            }
        }


        private string _table = "";
        public string Table
        {
            get
            {
                return _table;
            }

            set
            {
                _table = value;
                OnPropertyChanged(nameof(Table));
            }
        }

        private string _usedPoint = "0";
        public string UsedPoint
        {
            get { return _usedPoint; }
            set
            {
                if (_usedPoint != value)
                {
                    _usedPoint = value;
                    OnPropertyChanged(nameof(UsedPoint));
                }
            }
        }

        private int _totalAmount = 0;
        public int TotalAmount
        {
            get
            {
                return _totalAmount;
            }

            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }
        

        private DateTime? _receiptDate = null;
        public DateTime? ReceiptDate
        {
            get
            {
                return _receiptDate;
            }

            set
            {
                _receiptDate = value;
                OnPropertyChanged(nameof(ReceiptDate));
                if(_receiptDate != null)
                {
                    Receipts = new ObservableCollection<ReceiptModel>(receiptDAO.FindReceiptOnDate(ReceiptDate.Value));
                }
                else
                {
                    Receipts = new ObservableCollection<ReceiptModel>(receiptDAO.ReadAll());
                }
                FilterReceiptCommand receiptCommand = new FilterReceiptCommand(this, "search");
                receiptCommand.Search();
            }
        }

        private ObservableCollection<CustomerModel> _customer;
        public ObservableCollection<CustomerModel> Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        private ObservableCollection<DishModel> _listDish;
        public ObservableCollection<DishModel> ListDish
        {
            get
            {
                return _listDish;
            }
            set
            {
                _listDish = value;
                int index = 1;
                CombineListDish.Clear();
                foreach (var O in ListDish)
                {
                    CombineListDish.Add(new Tuple<DishModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(ListDish));

            }
        }
        private ObservableCollection<Tuple<DishModel, int>> _combineListDish = new ObservableCollection<Tuple<DishModel, int>>();
        public ObservableCollection<Tuple<DishModel, int>> CombineListDish
        {
            get
            {
                return _combineListDish;
            }
            set
            {
                _combineListDish = value;
                OnPropertyChanged(nameof(CombineListDish));
            }
        }
        private string _receiptSearch;
        public string ReceiptSearch
        {
            get { return _receiptSearch; }
            set { _receiptSearch = value; OnPropertyChanged(nameof(ReceiptSearch)); }
        }
        public ICommand DeleteReceiptCommand { get; }
        public ICommand ChoosedReceiptCommand { get; }
        public ICommand ChangeValueTexboxCommand { get; }

        public DateTime Date { get; set; }

        public AdminReceiptViewModel()
        {
            Date = DateTime.Now;
            Receipts = new ObservableCollection<ReceiptModel>(receiptDAO.ReadAll());
            DeleteReceiptCommand = new ReceiptClickCommand(this, "deleted");
            ChoosedReceiptCommand = new ReceiptClickCommand(this, "choose");
            ChangeValueTexboxCommand = new FilterReceiptCommand(this, "search");
        }
    }
}
