using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandSchedule
{
    internal class AddScheduleCommand : CommandBase
    {
        private AdminAddScheduleViewModel viewModel;
        private ScheduleDAO scheduleDAO = new ScheduleDAO();
        public AddScheduleCommand(AdminAddScheduleViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            ScheduleModel scheduleModel = new ScheduleModel();

            if (viewModel.Shift.Length < 11)
            {
                MessageBox.Show("Mã ca chưa đúng");
                return;
            }
            scheduleModel.shift = viewModel.Shift;
            scheduleModel.staff_number = viewModel.ListStaff.Count;
            scheduleModel.evaluate = new List<EvaluateModel>();
            foreach (var item in viewModel.ListStaff)
            {
                EvaluateModel evaluate = new EvaluateModel();
                evaluate.staff_id = item.staffid;
                evaluate.worked = false;
                scheduleModel.evaluate.Add(evaluate);
            }
            scheduleDAO.createSchedule(scheduleModel);
            MessageBox.Show("Đã thêm lịch làm");

        }
    }
}
