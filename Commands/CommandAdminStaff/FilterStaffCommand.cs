using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using DevCoffeeManagerApp.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandAdminStaff
{
    public class FilterStaffCommand : CommandBase
    {
        private AdminStaffViewModel viewModel;
        private string action;
        StaffDAO staffDAO = new StaffDAO();
        public FilterStaffCommand(AdminStaffViewModel viewModel, string action)
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
                case "search":
                    Search(parameter);
                    return;
            }
        }
        private void Search(object parameter) {
            ObservableCollection<StaffModel> StaffFilter = new ObservableCollection<StaffModel>();
            if (!string.IsNullOrWhiteSpace(viewModel.Staffsearch))
            {
                
                viewModel.Staffs = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
                foreach (StaffModel staff in viewModel.Staffs)
                {
                    
                    if (staff.phone_staff.Replace(" ", "").IndexOf(viewModel.Staffsearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0 || staff.staffname.Replace(" ", "").IndexOf(viewModel.Staffsearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        StaffFilter.Add(staff);
                    }
                }
                viewModel.Staffs = StaffFilter;
            }
            else
            {
                viewModel.Staffs = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
            }
        }
    }
}
