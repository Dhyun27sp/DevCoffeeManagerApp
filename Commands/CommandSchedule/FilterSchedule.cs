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
        private void Filter()
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
                ObservableCollection<ScheduleModel> scheduleModelsinmoth = new ObservableCollection<ScheduleModel>(scheduleDAO.findSchedulebymothCurrent());
                if (!string.IsNullOrWhiteSpace(viewModel.Schedulesearch))
                {
                    if(scheduleModelsinmoth.Count > 21)
                    {
                        int breakfor = 0;
                        for(int i = scheduleModelsinmoth.Count-1; i < scheduleModelsinmoth.Count; i--)
                        {
                            if (scheduleModelsinmoth[i].shift.Replace(" ", "").IndexOf(viewModel.Schedulesearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                scheduleModels.Add(scheduleModelsinmoth[i]);
                            }
                            else
                            {
                                scheduleModels.Add(scheduleModelsinmoth[i]);
                            }
                            breakfor++;
                            if (breakfor == 21) break;
                        }
                    }
                    else
                    {
                        foreach (ScheduleModel s in scheduleModelsinmoth)
                        {
                            if (s.shift.Replace(" ", "").IndexOf(viewModel.Schedulesearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                scheduleModels.Add(s);
                            }
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
        }
    }
}
