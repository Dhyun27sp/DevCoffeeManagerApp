using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using DevCoffeeManagerApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandSchedule
{
    internal class FilterSchedule:CommandBase
    {
        private AdminScheduleViewModel viewModel;
        private string action;
        StaffDAO staffDAO = new StaffDAO();
        ScheduleDAO scheduleDAO = new ScheduleDAO();
        public FilterSchedule(AdminScheduleViewModel viewModel, string action)
        {
            this.viewModel = viewModel;
            this.action = action;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (action)
            {
                case "cbb":
                    combbschedule();
                    return;
                case "search":
                    Search();
                    return;
            }

        }
        private void combbschedule()
        {
            Filter();
        }

        private void Search()
        {
            Filter();
        }
        public void Filter()
        {
            if (viewModel.itemcbb == "All")
            {
                ObservableCollection<ScheduleModel> scheduleModels = new ObservableCollection<ScheduleModel>();
                if (!string.IsNullOrWhiteSpace(viewModel.Schedulesearch))
                {
                    foreach (ScheduleModel s in viewModel.consoleListSchedule)
                    {
                        if(s.shift.Replace(" ", "").IndexOf(viewModel.Schedulesearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            scheduleModels.Add(s);
                        }
                    }
                    viewModel.Schedules = scheduleModels;
                    viewModel.Staffnotinschedule = new ObservableCollection<StaffModel>();
                    viewModel.Staffinschedule = new ObservableCollection<StaffModel>();
                    viewModel.evaluateWhenchooseSchedule = new List<EvaluateModel>();
                    viewModel.ScheduleclickCurrent = null;
                }
                else
                {
                    viewModel.Schedules = viewModel.consoleListSchedule;
                }
            }
            else if (viewModel.itemcbb == "On day")
            {
                viewModel.Schedules = new ObservableCollection<ScheduleModel>(scheduleDAO.findSchedulebyDayCurrent());
                viewModel.Staffnotinschedule = new ObservableCollection<StaffModel>();
                viewModel.Staffinschedule = new ObservableCollection<StaffModel>();
                viewModel.evaluateWhenchooseSchedule = new List<EvaluateModel>();
                viewModel.ScheduleclickCurrent = null;
            }
            else
            {
                ObservableCollection<ScheduleModel> scheduleModels = new ObservableCollection<ScheduleModel>();
                ObservableCollection<ScheduleModel> scheduleModelsinWeek = new ObservableCollection<ScheduleModel>(scheduleDAO.GetNSchedules(21));
                if (!string.IsNullOrWhiteSpace(viewModel.Schedulesearch))
                {
                    foreach (ScheduleModel s in scheduleModelsinWeek)
                    {
                        if (s.shift.Replace(" ", "").IndexOf(viewModel.Schedulesearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            scheduleModels.Add(s);
                        }
                    }
                    viewModel.Schedules = scheduleModels;
                    viewModel.Staffnotinschedule = new ObservableCollection<StaffModel>();
                    viewModel.Staffinschedule = new ObservableCollection<StaffModel>();
                    viewModel.evaluateWhenchooseSchedule = new List<EvaluateModel>();
                    viewModel.ScheduleclickCurrent = null;
                }
                else
                {
                    viewModel.Schedules = scheduleModelsinWeek;
                }
            }
        }
    }
}
