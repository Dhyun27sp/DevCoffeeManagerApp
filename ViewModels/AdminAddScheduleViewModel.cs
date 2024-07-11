using DevCoffeeManagerApp.Commands.CommandSchedule;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    internal class AdminAddScheduleViewModel : BaseViewModel
    {
        private string _shift;
        public string Shift
        {
            get { return _shift; }
            set
            {
                _shift = value;
                OnPropertyChanged(nameof(Shift));

            }
        }
        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                Shift = value.ToString("dd/MM/yyyy");
                if (Time != null)
                    Shift += Time[0];
                OnPropertyChanged(nameof(Date));
            }
        }
        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                Shift = Date.ToString("dd/MM/yyyy");
                if (value != null)
                {
                    _time = value;
                    Shift += Time[0];
                }
                OnPropertyChanged(nameof(Time));

            }
        }

        private ObservableCollection<StaffModel> _liststaff;
        public ObservableCollection<StaffModel> ListStaff
        {
            get
            {
                return _liststaff;
            }
            set
            {
                _liststaff = value;
                int index = 1;
                CombineListStaff.Clear();
                foreach (var O in ListStaff)
                {
                    CombineListStaff.Add(new Tuple<StaffModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(ListStaff));

            }
        }
        private ObservableCollection<Tuple<StaffModel, int>> _combineListstaff = new ObservableCollection<Tuple<StaffModel, int>>();
        public ObservableCollection<Tuple<StaffModel, int>> CombineListStaff
        {
            get
            {
                return _combineListstaff;
            }
            set
            {
                _combineListstaff = value;
                OnPropertyChanged(nameof(CombineListStaff));
            }
        }

        private ObservableCollection<StaffModel> _liststaffnotshift;
        public ObservableCollection<StaffModel> ListStaffNotShift
        {
            get
            {
                return _liststaffnotshift;
            }
            set
            {
                _liststaffnotshift = value;
                int index = 1;
                CombineListStaffNotShift.Clear();
                foreach (var O in ListStaffNotShift)
                {
                    CombineListStaffNotShift.Add(new Tuple<StaffModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(ListStaffNotShift));

            }
        }
        private ObservableCollection<Tuple<StaffModel, int>> _combineListstaffnotshift = new ObservableCollection<Tuple<StaffModel, int>>();
        public ObservableCollection<Tuple<StaffModel, int>> CombineListStaffNotShift
        {
            get
            {
                return _combineListstaffnotshift;
            }
            set
            {
                _combineListstaffnotshift = value;
                OnPropertyChanged(nameof(CombineListStaffNotShift));
            }
        }

        public ICommand AddScheduleCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ChoosedStaffCommand { get; set; }
        public ICommand ChoosedStaffNotShiftCommand { get; set; }
        public List<string> Times { get; set; }
        public AdminAddScheduleViewModel()
        {
            StaffDAO staffDAO = new StaffDAO(); 
            Times = new List<string>();
            Times.Add("Sáng");
            Times.Add("Chiều");
            Times.Add("Tối");
            AddScheduleCommand = new AddScheduleCommand(this);
            CloseCommand = new CloseCommand();
            ListStaff = new ObservableCollection<StaffModel>();
            ListStaffNotShift = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
            ChoosedStaffCommand = new ClickButtonCommand(this, "choosestaffremove");
            ChoosedStaffNotShiftCommand = new ClickButtonCommand(this, "choosestafftoadd");
        }
    }
}
