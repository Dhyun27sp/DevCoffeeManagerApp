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
        public AdminDiscountViewModel()
        {
            ChoosedDisCommand = new ClickButtonCommand(this, "choose");
            AddDisCommand = new ClickButtonCommand(this, "add");
            UpdateDisCommand = new ClickButtonCommand(this, "update");
            DeletefieldDisCommand = new ClickButtonCommand(this, "deletef");
            DeleteDisCommand = new ClickButtonCommand(this, "deleted");
            DiscountCbbCommand = new FilterDiscountCommand(this, "cbb");


            Discounts = new ObservableCollection<DiscountModel>(discountDAO.ReadDiscountAll());
            ExpiryDate = new List<string>();
            ExpiryDate.Add("All");
            ExpiryDate.Add("Expired");
            ExpiryDate.Add("valid");
        }
    }
}
