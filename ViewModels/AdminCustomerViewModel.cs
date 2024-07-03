using DevCoffeeManagerApp.Commands.AdminCommand.CustomerCommands;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminCustomerViewModel : BaseViewModel
    {
        CustomerDAO customerDAO = new CustomerDAO();

        private ObservableCollection<CustomerModel> _customers;
        public ObservableCollection<CustomerModel> Custommers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                int index = 1;
                CombineList.Clear();
                foreach (var O in Custommers)
                {
                    CombineList.Add(new Tuple<CustomerModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Custommers));

            }
        }
        private ObservableCollection<Tuple<CustomerModel, int>> _combineList = new ObservableCollection<Tuple<CustomerModel, int>>();
        public ObservableCollection<Tuple<CustomerModel, int>> CombineList
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
        private string _namecustom;
        public string NameCustom
        {
            get { return _namecustom; }
            set { _namecustom = value; OnPropertyChanged(nameof(NameCustom)); }
        }
        private string _phonecustom;
        public string PhoneCustom
        {
            get { return _phonecustom; }
            set { _phonecustom = value; OnPropertyChanged(nameof(PhoneCustom)); }
        }
        private DateTime? _dobcustom = null;
        public DateTime? Dobcustom
        {
            get { return _dobcustom; }
            set { _dobcustom = value; OnPropertyChanged(nameof(Dobcustom)); }
        }
        private int _point;
        public int Point
        {
            get { return _point; }
            set { _point = value; OnPropertyChanged(nameof(Point)); }
        }

        private bool _statusAdd;
        public bool StatusAdd {
            get { return _statusAdd; }
            set { _statusAdd = value; OnPropertyChanged(nameof(StatusAdd)); }

        }

        private string _itemcbb = "10 Customer";
        public string itemcbb
        {
            get { return _itemcbb; }
            set { _itemcbb = value; OnPropertyChanged(nameof(itemcbb)); }
        }
        private string _customersearch;
        public string Customersearch
        {
            get { return _customersearch; }
            set { _customersearch = value; OnPropertyChanged(nameof(Customersearch)); }
        }
        public ObservableCollection<CustomerModel> CustommersReadonly;
        public List<string> CustomersAction { get; set; }
        public ICommand ChoosedcusCommand { get; }
        public ICommand AddCusCommand { get; }
        public ICommand DeletefieldCusCommand { get; }
        public ICommand UpdateCusCommand { get; }
        public ICommand DeleteCusCommand { get; }
        public ICommand CustommerCbbCommand { get; }
        public ICommand ChangeValueTexboxCommand { get; }
        public DateTime Date { get; set; }

        public AdminCustomerViewModel() {
            Date = DateTime.Now;
            Custommers = customerDAO.GetAllCustomers();
            CustommersReadonly = Custommers;
            ChoosedcusCommand = new CustomerComand(this, "choose");
            AddCusCommand = new CustomerComand(this, "add");
            DeletefieldCusCommand = new CustomerComand(this, "deletef");
            UpdateCusCommand = new CustomerComand(this, "update");
            DeleteCusCommand = new CustomerComand(this, "deletec");

            CustommerCbbCommand = new FilterCustomerCommand(this, "cbb");
            ChangeValueTexboxCommand = new FilterCustomerCommand(this, "search");

            CustomersAction = new List<string>();
            CustomersAction.Add("10 Customer");
            CustomersAction.Add("100 Customer");
            CustomersAction.Add("All");
            CustomersAction.Add("Customer in Not action in year");
        }
    }
}
