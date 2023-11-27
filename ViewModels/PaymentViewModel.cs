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

        private bool _isMoMoChecked;
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

        private bool _isDirectPaymentChecked;
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

        private int _inputMoney;
        public int InputMoney
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

        private int _keepTheChange;
        public int KeepTheChange
        {
            get { return _keepTheChange; }
            set
            {
                if (_keepTheChange != value)
                {
                    _keepTheChange = value;
                    OnPropertyChanged(nameof(KeepTheChange));
                }
            }
        }

        private string _phonecustomer;
        public string Phonecustomer
        {
            get { return _phonecustomer; }
            set
            {
                if (_phonecustomer != value)
                {
                    _phonecustomer = value;
                    OnPropertyChanged(nameof(Phonecustomer));
                }
            }
        }

        private string _customername;
        public string Customername
        {
            get { return _customername; }
            set
            {
                if (_customername != value)
                {
                    _customername = value;
                    OnPropertyChanged(nameof(Customername));
                }
            }
        }

        private int _pointplus;
        public int Pointplus
        {
            get { return _pointplus; }
            set
            {
                if (_pointplus != value)
                {
                    _pointplus = value;
                    OnPropertyChanged(nameof(Pointplus));
                }
            }
        }
        
        private int _pointminus;
        public int Pointminus
        {
            get { return _pointminus; }
            set
            {
                if (_pointminus != value)
                {
                    _pointminus = value;
                    OnPropertyChanged(nameof(Pointminus));
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
        public ICommand SubmitPayCommand { get; set; }
        public PaymentViewModel() {
            /*SubmitPayCommand = new tự tạo class Command*/
            if (SessionStatic.GetOrdereds != null)
            {
                OrderedFood = SessionStatic.GetOrdereds;
            }
        }
    }
}
