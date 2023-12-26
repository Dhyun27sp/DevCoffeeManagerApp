using DevCoffeeManagerApp.Commands.CommandReceipt;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class ReceiptViewModel : BaseViewModel
    {
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

        private string _customerName = "";
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
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

        private string _receiptCode;
        public string ReceiptCode
        {
            get { return _receiptCode; }
            set
            {
                if (_receiptCode != value)
                {
                    _receiptCode = value;
                    OnPropertyChanged(nameof(ReceiptCode));
                }
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

        public ICommand PrintReceiptCommand { get; set; }
        public ICommand CloseCommand { get; }

        public ReceiptViewModel() {
            CloseCommand = new CloseCommand();
            PrintReceiptCommand = new PrintReceiptCommand();
            if (SessionStatic.Customer != null)
            {
                CustomerName = SessionStatic.GetReceipt.customer.name;
            }
            if (SessionStatic.GetReceipt != null) {
                ReceiptCode = SessionStatic.GetReceipt.receipt_code;
                UsedPoint = SessionStatic.GetReceipt.used_point.ToString();
                CurrentDate = SessionStatic.GetReceipt.time;
                StaffPhoneNumber = SessionStatic.GetReceipt.staff_phone;
                TotalAmount = SessionStatic.GetReceipt.total_amount;
                Ordereds = new ObservableCollection<DishModel> (SessionStatic.GetReceipt.Dishes);
            }


            if (SessionStatic.GetTables != null)
            {
                string tables = "";
                foreach (var i in SessionStatic.GetTables)
                {
                    if (tables == "")
                    {
                        tables = i.No_.ToString();
                    }
                    else
                        tables = tables + ", " + i.No_.ToString();
                }
                Table = tables;
            }
        }
    }
}
