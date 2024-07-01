using DevCoffeeManagerApp.Commands.CommandPayment;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class PaymentViewModel:BaseViewModel
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
                    if (O.Saleprice != null)
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

        private bool _isMoMoChecked = false;
        public bool IsMoMoChecked
        {
            get { return _isMoMoChecked; }
            set
            {
                if (_isMoMoChecked != value)
                {
                    _isMoMoChecked = value;
                    OnPropertyChanged(nameof(IsMoMoChecked));
                }
            }
        }

        private bool _isDirectPaymentChecked = false;
        public bool IsDirectPaymentChecked
        {
            get { return _isDirectPaymentChecked; }
            set
            {
                if (_isDirectPaymentChecked != value)
                {
                    _isDirectPaymentChecked = value;
                    OnPropertyChanged(nameof(IsDirectPaymentChecked));
                }
            }
        }

        private string _inputMoney = "0";
        public string InputMoney
        {
            get { return _inputMoney; }
            set
            {
                if (_inputMoney != value)
                {
                    _inputMoney = value;
                    OnPropertyChanged(nameof(InputMoney));
                }
            }
        }

        private int _change = 0;
        public int Change // tiền thối
        {
            get { return _change; }
            set
            {
                if (_change != value)
                {
                    _change = value;
                    OnPropertyChanged(nameof(Change));
                }
            }
        }

        private string _customerPhonenumber;
        public string CustomerPhoneNumber
        {
            get { return _customerPhonenumber; }
            set
            {
                if (_customerPhonenumber != value)
                {
                    _customerPhonenumber = value;
                    OnPropertyChanged(nameof(CustomerPhoneNumber));
                }
            }
        }

        private string _customerName;
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

        private int _plusPoint;
        public int PlusPoint
        {
            get { return _plusPoint; }
            set
            {
                if (_plusPoint != value)
                {
                    _plusPoint = value;
                    OnPropertyChanged(nameof(PlusPoint));
                }
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

        private string _additionalinfor;
        public string Additionalinfor
        {
            get { return _additionalinfor; }
            set
            {
                if (_additionalinfor != value)
                {
                    _additionalinfor = value;
                    OnPropertyChanged(nameof(Additionalinfor));
                }
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
        public ICommand SelectionchangeInputMoney { get; set; }
        public ICommand SubmitPaymentCommand { get; set; }
        public ICommand CheckPaymentCommand { get; set; }
        public PaymentViewModel() {
            SelectionchangeInputMoney = new MoneyReceivedCommand(this);
            SubmitPaymentCommand = new SubmitPaymentCommand(this);
            CheckPaymentCommand = new CheckPaymentCommand();
            if (SessionStatic.Customer != null)
            {
                CustomerName = SessionStatic.Customer.name;
                CustomerPhoneNumber = SessionStatic.Customer.phone_number;
                PlusPoint = SessionStatic.Customer.pluspoint;
                UsedPoint = SessionStatic.Customer.usedpoint;
            }
            
            if (SessionStatic.GetOrdereds != null)
            {
                OrderedFood = SessionStatic.GetOrdereds;
                foreach(var order in OrderedFood)
                {
                    Total = Total + order.Amount;
                }    
            }
            TotalAmount = Total - Int32.Parse(UsedPoint);
            CurrentDate = DateTime.Now;
            StaffPhoneNumber = SessionStatic.GetPhoneNumber;
        }
    }
}
