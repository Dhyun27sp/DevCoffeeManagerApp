using DevCoffeeManagerApp.Commands.CommandCheck;
using DevCoffeeManagerApp.Models;
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
    public class CheckViewModel : BaseViewModel
    {
        private string _url= "https://www.google.com/maps/@41.2950114,-91.9826847,14z";
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));

            }
        }

        private string _receiptcode;
        public string ReceiptCode
        {
            get { return _receiptcode; }
            set
            {
                _receiptcode = value;
                OnPropertyChanged(nameof(ReceiptCode));

            }
        }

        private string _orderId;
        public string OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }

        private string _cusName;
        public string CusName
        {
            get { return _cusName; }
            set
            {
                _cusName = value;
                OnPropertyChanged(nameof(CusName));
            }
        }

        private string _cusPhone;
        public string CusPhone
        {
            get { return _cusPhone; }
            set
            {
                _cusPhone = value;
                OnPropertyChanged(nameof(CusPhone));
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
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
        public ICommand CheckCommand { get; set; }
        public CheckViewModel(NavigationStore navigationStore)
        {
            CheckCommand = new CheckCommand(this);
        }

    }
}
