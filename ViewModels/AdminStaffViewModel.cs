using DevCoffeeManagerApp.Commands.CommandAdminStaff;
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
    public class AdminStaffViewModel : BaseViewModel
    {
        StaffDAO staffDAO = new StaffDAO();
        private ObservableCollection<StaffModel> _staff;
        public ObservableCollection<StaffModel> Staffs
        {
            get
            {
                return _staff;
            }
            set
            {
                _staff = value;
                int index = 1;
                CombineList.Clear();
                foreach (var O in Staffs)
                {
                    SalaryModel salarynew  = new SalaryModel();
                    SalaryModel maxmonth = new SalaryModel();
                    if (O.salary.Count != 0)
                    {
                        maxmonth = O.salary[0];
                    }
                    foreach (SalaryModel salary in O.salary)
                    {
                        if (maxmonth.Month.Month < salary.Month.Month)
                        {
                            maxmonth = salary;
                        }
                    }
                    CombineList.Add(new Tuple<StaffModel, int, string, int>(O, index, O.salary.Count != 0 ? maxmonth.Month.Month.ToString():"", maxmonth.Money));
                    index++;
                }
                OnPropertyChanged(nameof(Staffs));

            }
        }
        
        private ObservableCollection<Tuple<StaffModel, int, string, int>> _combineList = new ObservableCollection<Tuple<StaffModel, int, string, int>>();
        public ObservableCollection<Tuple<StaffModel, int, string, int>> CombineList
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
        private ObservableCollection<SalaryModel> _detailsalary;
        public ObservableCollection<SalaryModel> DetailSalary
        {
            get
            {
                return _detailsalary;
            }
            set
            {
                _detailsalary = value;
                int index = 1;
                CombineListDetailSalary.Clear();
                foreach (var O in DetailSalary)
                {
                    CombineListDetailSalary.Add(new Tuple<SalaryModel, int, string, string >(O, index,O.Month.Month.ToString(),O.Month.Year.ToString()));
                    index++;
                }
                OnPropertyChanged(nameof(DetailSalary));

            }
        }
        private ObservableCollection<Tuple<SalaryModel, int, string, string>> _combineListdetailsalary = new ObservableCollection<Tuple<SalaryModel, int, string, string>>();
        public ObservableCollection<Tuple<SalaryModel, int, string, string>> CombineListDetailSalary
        {
            get
            {
                return _combineListdetailsalary;
            }
            set
            {
                _combineListdetailsalary = value;
                OnPropertyChanged(nameof(CombineListDetailSalary));
            }
        }

        private string _namestaff = "";
        public string NameStaff
        {
            get
            {
                return _namestaff;
            }
            set
            {
                _namestaff = value;
                OnPropertyChanged(nameof(NameStaff));
            }
        }
        private string _phonestaff = "";
        public string PhoneStaff
        {
            get
            {
                return _phonestaff;
            }
            set
            {
                _phonestaff = value;
                OnPropertyChanged(nameof(PhoneStaff));
            }
        }
        private int? _password = null;
        public int? PasswordStaff
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(PasswordStaff));
            }
        }
        private bool _statusupdate = false;
        public bool StatusUpdate
        {
            get
            {
                return _statusupdate;
            }
            set
            {
                _statusupdate = value;
                OnPropertyChanged(nameof(StatusUpdate));
            }
        }
        private string _staffsearch = "";
        public string Staffsearch
        {
            get
            {
                return _staffsearch;
            }
            set
            {
                _staffsearch = value;
                OnPropertyChanged(nameof(Staffsearch));
            }
        }
        
        public ICommand ChoosedStaffCommand { get; }
        public ICommand AddStaffCommand { get; }
        public ICommand DeleteStaffCommand { get; }
        public ICommand DeletefieldStaffCommand { get; }
        public ICommand UpdateStaffCommand { get; }
        public ICommand ChangeValueTexboxCommand { get; }
        public AdminStaffViewModel() {
            Staffs = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
            staffDAO.Createsalary();// tạo tháng lương cho nhân viên
            staffDAO.salaryInMoth();
            ChoosedStaffCommand = new CommandClickStaffAd(this, "choose");
            AddStaffCommand = new CommandClickStaffAd(this, "add");
            DeleteStaffCommand = new CommandClickStaffAd(this, "deletestaff");
            DeletefieldStaffCommand = new CommandClickStaffAd(this, "deletef");
            UpdateStaffCommand = new CommandClickStaffAd(this, "update");
            ChangeValueTexboxCommand = new FilterStaffCommand(this, "search");
        }
    }
}
