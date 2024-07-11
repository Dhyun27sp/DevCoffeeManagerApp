using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandSchedule
{
    internal class ClickButtonCommand: CommandBase
    {
        private AdminAddScheduleViewModel viewModel;
        private string action;
        DiscountDAO discountDAO = new DiscountDAO();
        MenuDAO menuDao = new MenuDAO();
        public ClickButtonCommand(AdminAddScheduleViewModel viewModel, string action)
        {
            this.action = action;
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (action)
            {                
                case "choosestaffremove":
                    choosestaffapplytoremove(parameter);
                    return;
                case "choosestafftoadd":
                    choosestaffapplytoadd(parameter);
                    return;
            }
        }

        private void choosestaffapplytoremove(object parameter)
        {
            if (parameter is ListView listdish)
            {
                if (listdish.SelectedItem != null)
                {
                    Tuple<StaffModel, int> selectedItem = (Tuple<StaffModel, int>)listdish.SelectedItem;
                    MessageBox.Show("Xóa nhân viên Trong danh sách thành công");
                    viewModel.ListStaff.Remove(selectedItem.Item1);
                    viewModel.ListStaffNotShift.Add(selectedItem.Item1);
                    viewModel.ListStaff = viewModel.ListStaff;
                    viewModel.ListStaffNotShift = viewModel.ListStaffNotShift;
                }
            }
        }
        private void choosestaffapplytoadd(object parameter)
        {
            if (parameter is ListView listdish)
            {
                if (listdish.SelectedItem != null)
                {
                    Tuple<StaffModel, int> selectedItem = (Tuple<StaffModel, int>)listdish.SelectedItem;
                    MessageBox.Show("Thêm Món vào Giảm giá thành công");
                    viewModel.ListStaff.Add(selectedItem.Item1);
                    viewModel.ListStaffNotShift.Remove(selectedItem.Item1);
                    viewModel.ListStaff = viewModel.ListStaff;
                    viewModel.ListStaffNotShift = viewModel.ListStaffNotShift;
                }
            }
        }
    }
}
