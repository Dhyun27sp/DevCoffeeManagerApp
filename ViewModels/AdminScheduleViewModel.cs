using DevCoffeeManagerApp.Commands.CommandSchedule;
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
    public class AdminScheduleViewModel : BaseViewModel
    {
        ScheduleDAO scheduleDAO = new ScheduleDAO();
        private ObservableCollection<ScheduleModel> _schedule;
        public ObservableCollection<ScheduleModel> Schedules
        {
            get
            {
                return _schedule;
            }
            set
            {
                _schedule = value;
                int index = 1;
                CombineListSchedule.Clear();
                foreach (var O in Schedules)
                {
                    O.staff_number = O.evaluate.Count;
                    CombineListSchedule.Add(new Tuple<ScheduleModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Schedules));

            }
        }
        private ObservableCollection<Tuple<ScheduleModel, int>> _combineListschedule = new ObservableCollection<Tuple<ScheduleModel, int>>();
        public ObservableCollection<Tuple<ScheduleModel, int>> CombineListSchedule
        {
            get
            {
                return _combineListschedule;
            }
            set
            {
                _combineListschedule = value;
                OnPropertyChanged(nameof(CombineListSchedule));
            }
        }


        private ObservableCollection<StaffModel> _staffnotinschedule;
        public ObservableCollection<StaffModel> Staffnotinschedule
        {
            get
            {
                return _staffnotinschedule;
            }
            set
            {
                _staffnotinschedule = value;
                int index = 1;
                CombineListStaffNotInSchedule.Clear();
                foreach (var O in Staffnotinschedule)
                {
                    CombineListStaffNotInSchedule.Add(new Tuple<StaffModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Staffnotinschedule));

            }
        }
        private ObservableCollection<Tuple<StaffModel, int>> _combineListstaffnotinschedule = new ObservableCollection<Tuple<StaffModel, int>>();
        public ObservableCollection<Tuple<StaffModel, int>> CombineListStaffNotInSchedule
        {
            get
            {
                return _combineListstaffnotinschedule;
            }
            set
            {
                _combineListstaffnotinschedule = value;
                OnPropertyChanged(nameof(CombineListStaffNotInSchedule));
            }
        }
        private ObservableCollection<Tuple<StaffModel, int,string>> _combineListstaffinschedule = new ObservableCollection<Tuple<StaffModel, int,string>>();
        public ObservableCollection<Tuple<StaffModel, int,string>> CombineListStaffInSchedule
        {
            get
            {
                return _combineListstaffinschedule;
            }
            set
            {
                _combineListstaffinschedule = value;
                OnPropertyChanged(nameof(CombineListStaffInSchedule));
            }
        }

        private ObservableCollection<StaffModel> _staffinschedule;
        public ObservableCollection<StaffModel> Staffinschedule
        {
            get
            {
                return _staffinschedule;
            }
            set
            {
                _staffinschedule = value;
                int index = 1;
                CombineListStaffInSchedule.Clear();
                foreach (var O in Staffinschedule)
                {
                    if(evaluateWhenchooseSchedule != null)
                    {
                        foreach (var e in evaluateWhenchooseSchedule)
                        {
                            if(e.staff_id == O.staffid)
                            {
                                if(e.worked == true)
                                {
                                    CombineListStaffInSchedule.Add(new Tuple<StaffModel, int, string>(O, index, "V"));
                                    index++;
                                }
                                else
                                {
                                    CombineListStaffInSchedule.Add(new Tuple<StaffModel, int, string>(O, index, "X"));
                                    index++;
                                }
                            }
                        }
                    }
                    else
                    {
                        CombineListStaffInSchedule.Add(new Tuple<StaffModel, int, string>(O, index, ""));
                        index++;
                    }
                }
                OnPropertyChanged(nameof(Staffinschedule));

            }
        }
        private ScheduleModel _schedulecurent;
        public ScheduleModel ScheduleclickCurrent
        {
            get { return _schedulecurent; }
            set { _schedulecurent = value; OnPropertyChanged(nameof(ScheduleclickCurrent)); }
        }

        public List<string> ScheduleIntime { get; set; }
        
        private string _itemcbb = "On day";
        public string itemcbb
        {
            get { return _itemcbb; }
            set
            {
                if (_itemcbb != value)
                {
                    _itemcbb = value;
                    OnPropertyChanged(nameof(itemcbb));
                }
            }
        }
        private string _schedulesearch;
        public string Schedulesearch
        {
            get { return _schedulesearch; }
            set { _schedulesearch = value; OnPropertyChanged(nameof(Schedulesearch)); }
        }
        public List<EvaluateModel> evaluateWhenchooseSchedule { get; set; }
        public ICommand ChoosedScheduleCommand { get; }
        public ICommand AddSchedule { get; }
        public ICommand ChoosedStafftoShiftCommand { get; }
        public ICommand RemoveStaffOutScheduleCommand { get; }
        public ICommand DeleteScheduleCommand { get; }
        public ICommand ScheduleCbbCommand { get; }
        public ICommand ChangeValueTexboxCommand { get; }
        public ObservableCollection<ScheduleModel> consoleListSchedule;
        public AdminScheduleViewModel() {
            Schedules = new ObservableCollection<ScheduleModel>(scheduleDAO.GetAllSchedule());
            consoleListSchedule = Schedules;
            ChoosedScheduleCommand = new ClickCommandSchedule(this, "choose");
            AddSchedule = new ClickCommandSchedule(this, "add");
            ChoosedStafftoShiftCommand = new ClickCommandSchedule(this, "addstaff");
            RemoveStaffOutScheduleCommand = new ClickCommandSchedule(this, "removestaff");
            DeleteScheduleCommand = new ClickCommandSchedule(this, "delete");

            ScheduleCbbCommand = new FilterSchedule(this, "cbb");
            ChangeValueTexboxCommand = new FilterSchedule(this,"search");

            ScheduleIntime = new List<string>();
            ScheduleIntime.Add("All");
            ScheduleIntime.Add("On day");
            ScheduleIntime.Add("On Week");
        }
    }
}
