using DevCoffeeManagerApp.Commands.CommandBook;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class BookViewModel : BaseViewModel
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

        private string _receiptcode;
        public string ReceiptCode
        {
            get
            {
                return _receiptcode;
            }

            set
            {
                _receiptcode = value;
                OnPropertyChanged(nameof(ReceiptCode));
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
        private string _quotation;
        public string Quotation
        {
            get { return _quotation; }
            set
            {
                _quotation = value;
                OnPropertyChanged(nameof(Quotation));
            }
        }
        public ICommand BookCommand { get; set; }
        public BookViewModel(NavigationStore navigationStore)
        {
            ReceiptDAO receiptDAO = new ReceiptDAO();
            ReceiptModel receiptModel = SessionStatic.GetReceipt;
            ReceiptCode = receiptModel.receipt_code;
            CusName = SessionStatic.CusContact.Name;
            CusPhone = SessionStatic.CusContact.Phone;
            Quotation = SessionStatic.QuotationId;
            BookCommand = new BookCommand(this);
            OrderedFood = new ObservableCollection<DishModel>(receiptDAO.FindOrderbyReceiptCode(receiptModel.receipt_code));
        }

    }
}
