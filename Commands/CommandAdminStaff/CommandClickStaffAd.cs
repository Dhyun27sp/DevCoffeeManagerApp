using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandAdminStaff
{
    internal class CommandClickStaffAd : CommandBase
    {
        private AdminStaffViewModel viewModel;
        private string action;
        public CommandClickStaffAd(AdminStaffViewModel viewModel, string action)
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
                case "choose":
                    chooseStaff(parameter);
                    return;
            }
        }
        private void chooseStaff(object parameter)
        {
            if (parameter is ListView liststaff)
            {
                if (liststaff.SelectedItem != null)
                {
                    Tuple<StaffModel, int, string, int> selectedItem = (Tuple<StaffModel, int, string, int>)liststaff.SelectedItem;
                    viewModel.NameStaff = selectedItem.Item1.staffname;
                    viewModel.PhoneStaff = selectedItem.Item1.phone_staff;
                    viewModel.PasswordStaff = selectedItem.Item1.account.Password;
                    viewModel.DetailSalary = new ObservableCollection<SalaryModel>(selectedItem.Item1.salary);
                }
            }
        }
    }
}
