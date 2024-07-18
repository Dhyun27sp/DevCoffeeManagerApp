using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandSchedule
{
    internal class ClickCommandSchedule :CommandBase
    {
        private AdminScheduleViewModel viewModel;
        private string action;
        StaffDAO staffDAO = new StaffDAO();
        ScheduleDAO scheduleDAO = new ScheduleDAO();
        public ClickCommandSchedule(AdminScheduleViewModel viewModel, string action)
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
                case "choose":
                    chooseschedule(parameter);
                    return;
                case "delete":
                    DeleteS(parameter);
                    return;
                case "addstaff":
                    addstaff(parameter);
                    return;
                case "removestaff":
                    removestaff(parameter);
                    return;
            }
        }
        private void chooseschedule(object parameter)
        {
            if (parameter is ListView listschedule)
            {
                if (listschedule.SelectedItem != null)
                {
                    Tuple<ScheduleModel, int> selectedItem = (Tuple<ScheduleModel, int>)listschedule.SelectedItem;
                    viewModel.ScheduleclickCurrent = selectedItem.Item1;
                    viewModel.Staffinschedule = new ObservableCollection<StaffModel>(); 
                    foreach (EvaluateModel e in selectedItem.Item1.evaluate)
                    {
                        viewModel.Staffinschedule.Add(staffDAO.GetStaffbyid(e.staff_id));
                    }
                    viewModel.evaluateWhenchooseSchedule = selectedItem.Item1.evaluate;
                    viewModel.Staffinschedule = viewModel.Staffinschedule;
                    ObservableCollection<StaffModel> AllStaff = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
                    var result = AllStaff.Where(cm => !viewModel.Staffinschedule.Any(cma => cma.staffid == cm.staffid)).ToList();
                    viewModel.Staffnotinschedule = new ObservableCollection<StaffModel>(result);
                }
            }
        }
        private void addstaff(object parameter)
        {
            if (parameter is ListView liststaff)
            {
                if (liststaff.SelectedItem != null && viewModel.Date < DateTime.Now)
                {
                    Tuple<StaffModel, int> selectedItem = (Tuple<StaffModel, int>)liststaff.SelectedItem;
                    EvaluateModel evaluate = new EvaluateModel();
                    evaluate.staff_id = selectedItem.Item1.staffid;
                    evaluate.worked = false;
                    scheduleDAO.AddEvaluteWithIdSchedule(evaluate,viewModel.ScheduleclickCurrent.ScheduleId);
                    viewModel.evaluateWhenchooseSchedule.Add(evaluate);
                    viewModel.Staffinschedule.Add(selectedItem.Item1);
                    viewModel.Staffinschedule = viewModel.Staffinschedule;
                    viewModel.Schedules = viewModel.Schedules;
                    viewModel.Staffnotinschedule.Remove(selectedItem.Item1);
                    viewModel.Staffnotinschedule = viewModel.Staffnotinschedule;
                }
            }
        }
        private void removestaff(object parameter)
        {
            if (parameter is ListView liststaff)
            {
                if (liststaff.SelectedItem != null && DateTime.Parse(viewModel.ScheduleclickCurrent.shift) < DateTime.Now)
                {
                    Tuple<StaffModel, int,string> selectedItem = (Tuple<StaffModel, int,string>)liststaff.SelectedItem;
                    EvaluateModel evaluate = new EvaluateModel();
                    evaluate.staff_id = selectedItem.Item1.staffid;
                    evaluate.worked = false;
                    scheduleDAO.RemoveEvaluateFromSchedule(evaluate, viewModel.ScheduleclickCurrent.ScheduleId);
                    foreach(EvaluateModel e in viewModel.evaluateWhenchooseSchedule)
                    {
                        if(evaluate.staff_id == e.staff_id)
                        {
                            viewModel.evaluateWhenchooseSchedule.Remove(e);
                            break;
                        }
                    }
                    viewModel.Staffinschedule.Remove(selectedItem.Item1);
                    viewModel.Staffinschedule = viewModel.Staffinschedule;
                    viewModel.Staffnotinschedule.Add(selectedItem.Item1);
                    viewModel.Staffnotinschedule = viewModel.Staffnotinschedule;
                    viewModel.Schedules = viewModel.Schedules;
                }
            }
        }
        private void DeleteS(object parameter)
        {
            if (parameter is ScheduleModel schedule)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if(schedule == viewModel.ScheduleclickCurrent)
                    {
                        viewModel.Staffnotinschedule = new ObservableCollection<StaffModel>();
                        viewModel.Staffinschedule = new ObservableCollection<StaffModel>();
                        viewModel.evaluateWhenchooseSchedule = new List<EvaluateModel>();
                        viewModel.ScheduleclickCurrent = null;
                    }
                    scheduleDAO.DeleteScheduleByid(schedule.ScheduleId);
                    viewModel.Schedules.Remove(schedule);
                    MessageBox.Show("Xóa thành công");
                    viewModel.Schedules = viewModel.Schedules;
                }
            }
        }
    }
}
