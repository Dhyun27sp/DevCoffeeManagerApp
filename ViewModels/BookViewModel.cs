using DevCoffeeManagerApp.Commands.CommandBook;
using DevCoffeeManagerApp.DAOs;
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
        public ICommand BookCommand { get; set; }
        public BookViewModel(NavigationStore navigationStore)
        {
            ReceiptDAO receiptDAO = new ReceiptDAO();
            ReceiptModel receiptModel = SessionStatic.GetReceipt;
            BookCommand = new BookCommand(this);
            //OrderedFood = new ObservableCollection<DishModel>(receiptDAO.FindOrderbyReceiptCode(receiptModel.receipt_code));
            //OrderedFood = SessionStatic.GetOrdereds;
        }

    }
}
