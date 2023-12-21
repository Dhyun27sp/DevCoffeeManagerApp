using DevCoffeeManagerApp.Commands.AdminCommand.DiscountCommands;
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
    public class AdminDiscountViewModel : BaseViewModel
    {
        DiscountDAO discountDAO = new DiscountDAO();
        MenuDAO menuDao = new MenuDAO();

        //Khối Property và ICommand Hộ Trợ DistcountList : End
        private ObservableCollection<DiscountModel> _discount;
        public ObservableCollection<DiscountModel> Discounts
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
                int index = 1;
                CombineList.Clear();
                foreach (var O in Discounts)
                {
                    CombineList.Add(new Tuple<DiscountModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Discounts));

            }
        }
        private ObservableCollection<Tuple<DiscountModel, int>> _combineList = new ObservableCollection<Tuple<DiscountModel, int>>();
        public ObservableCollection<Tuple<DiscountModel, int>> CombineList
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
        private string _discountid;
        public string DiscountID
        {
            get { return _discountid; }
            set { _discountid = value; OnPropertyChanged(nameof(DiscountID)); }
        }
        private string _discountname;
        public string Discountname
        {
            get { return _discountname; }
            set { _discountname = value; OnPropertyChanged(nameof(Discountname)); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }
        private DateTime? _daystart = null;
        public DateTime? Daystart
        {
            get { return _daystart; }
            set { _daystart = value; OnPropertyChanged(nameof(Daystart)); }
        }
        private DateTime? _dayend = null;
        public DateTime? Dayend
        {
            get { return _dayend; }
            set { _dayend = value; OnPropertyChanged(nameof(Dayend)); }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(nameof(Value)); }
        }
        private bool _notallowedadd;
        public bool NotallowedAdd
        {
            get { return _notallowedadd; }
            set { _notallowedadd = value; OnPropertyChanged(nameof(NotallowedAdd)); }

        }
        private List<string> _expiryDate;
        public List<string> ExpiryDate
        {
            get { return _expiryDate; }
            set { _expiryDate = value; OnPropertyChanged(nameof(ExpiryDate)); }

        }
        private string _expiryDateitem = "All";
        public string ExpiryDateitem
        {
            get { return _expiryDateitem; }
            set { _expiryDateitem = value; OnPropertyChanged(nameof(ExpiryDateitem)); }
        }
        public ICommand ChoosedDisCommand { get; }
        public ICommand AddDisCommand { get; }
        public ICommand UpdateDisCommand { get; }
        public ICommand DeletefieldDisCommand { get; }
        public ICommand DeleteDisCommand { get; }
        public ICommand DiscountCbbCommand { get; }

        //Khối Property và Command Hộ Trợ DistcountList : End
        /// <summary>
        /// /////////
        /// </summary>
        //Khối Property và Command Hộ Trợ DishList : Start
        private List<string> _typeDishs;
        public List<string> TypeDishs
        {
            get { return _typeDishs; }
            set { _typeDishs = value; OnPropertyChanged(nameof(TypeDishs)); }

        }
        private string _typeDishitem = "All Dishs";
        public string TypeDishitem
        {
            get { return _typeDishitem; }
            set { _typeDishitem = value; OnPropertyChanged(nameof(TypeDishitem)); }
        }
        private ObservableCollection<DishModel> _listdishs;
        public ObservableCollection<DishModel> ListDishs
        {
            get
            {
                return _listdishs;
            }
            set
            {
                _listdishs = value;
                int index = 1;
                CombineListDish.Clear();
                foreach (var O in ListDishs)
                {
                    CombineListDish.Add(new Tuple<DishModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(ListDishs));

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

        private ObservableCollection<DishModel> _listdishsnotdc;
        public ObservableCollection<DishModel> ListDishsNotDC
        {
            get
            {
                return _listdishsnotdc;
            }
            set
            {
                _listdishsnotdc = value;
                int index = 1;
                CombineListDishNotDC.Clear();
                foreach (var O in ListDishsNotDC)
                {
                    CombineListDishNotDC.Add(new Tuple<DishModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(ListDishsNotDC));

            }
        }
        private ObservableCollection<Tuple<DishModel, int>> _combineListDishnotdc = new ObservableCollection<Tuple<DishModel, int>>();
        public ObservableCollection<Tuple<DishModel, int>> CombineListDishNotDC
        {
            get
            {
                return _combineListDishnotdc;
            }
            set
            {
                _combineListDishnotdc = value;
                OnPropertyChanged(nameof(CombineListDishNotDC));
            }
        }
        //Khối Property và Command Hộ Trợ DishList : End
        /// <summary>
        /// /////////
        /// </summary>
        //Khối Property và Command Hộ Trợ MenuList : Start
        private ObservableCollection<MenuModel> _listmenu;
        public ObservableCollection<MenuModel> ListMenu
        {
            get
            {
                return _listmenu;
            }
            set
            {
                _listmenu = value;
                int index = 1;
                CombineListMenu.Clear();
                foreach (var O in ListMenu)
                {
                    CombineListMenu.Add(new Tuple<MenuModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(ListMenu));

            }
        }
        private ObservableCollection<Tuple<MenuModel, int>> _combineListmenu = new ObservableCollection<Tuple<MenuModel, int>>();
        public ObservableCollection<Tuple<MenuModel, int>> CombineListMenu
        {
            get
            {
                return _combineListmenu;
            }
            set
            {
                _combineListmenu = value;
                OnPropertyChanged(nameof(CombineListMenu));
            }
        }

        private ObservableCollection<MenuModel> _listmenunotdc;
        public ObservableCollection<MenuModel> ListMenuNotDC
        {
            get
            {
                return _listmenunotdc;
            }
            set
            {
                _listmenunotdc = value;
                int index = 1;
                CombineListMenuNotDC.Clear();
                foreach (var O in ListMenuNotDC)
                {
                    CombineListMenuNotDC.Add(new Tuple<MenuModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(ListMenuNotDC));

            }
        }
        private ObservableCollection<Tuple<MenuModel, int>> _combineListmenunotdc = new ObservableCollection<Tuple<MenuModel, int>>();
        public ObservableCollection<Tuple<MenuModel, int>> CombineListMenuNotDC
        {
            get
            {
                return _combineListmenunotdc;
            }
            set
            {
                _combineListmenunotdc = value;
                OnPropertyChanged(nameof(CombineListMenuNotDC));
            }
        }
        //Khối Property và Command Hộ Trợ MenuList : End
        public AdminDiscountViewModel()
        {
            //Khối Config Hộ Trợ DistcountList :Start
            DeletefieldDisCommand = new ClickButtonCommand(this, "deletef");
            DiscountCbbCommand = new FilterDiscountCommand(this, "cbb");
            ChoosedDisCommand = new ClickButtonCommand(this, "choose");
            DeleteDisCommand = new ClickButtonCommand(this, "deleted");
            UpdateDisCommand = new ClickButtonCommand(this, "update");
            AddDisCommand = new ClickButtonCommand(this, "add");
            Discounts = new ObservableCollection<DiscountModel>(discountDAO.ReadDiscountAll());
            ExpiryDate = new List<string>();
            ExpiryDate.Add("All");
            ExpiryDate.Add("Expired");
            ExpiryDate.Add("valid");
            //Khối Config Hộ Trợ DistcountList : End
            ////////////////////////////
            //Khối Config Hộ Trợ DishList :Start
            ObservableCollection<MenuModel> menuModels = menuDao.ReadAll_Type_dish();
            TypeDishs = new List<string>();
            TypeDishs.Add("All Dishs");
            foreach (var item in menuModels)
            {
                TypeDishs.Add(item.type_of_dish);
            }
        }

    }
}
