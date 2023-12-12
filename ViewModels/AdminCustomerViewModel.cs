using DevCoffeeManagerApp.Commands.AdminCommand.CustomerCommands;
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
        public ICommand ChoosedcusCommand { get; }
        public ICommand AddCusCommand { get; }
        public ICommand DeletefieldCusCommand { get; }
        public ICommand UpdateCusCommand { get; }
        public ICommand DeleteCusCommand { get; }
        public AdminCustomerViewModel() {
            Custommers = customerDAO.GetAllCustomers();
            ChoosedcusCommand = new CustomerComand(this, "choose");
            AddCusCommand = new CustomerComand(this, "add");
            DeletefieldCusCommand = new CustomerComand(this, "deletef");
            UpdateCusCommand = new CustomerComand(this, "update");
            DeleteCusCommand = new CustomerComand(this, "deletec");
        }
    }
}
