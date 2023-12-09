using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.AdminCommand.NavigateSidebarCommand
{
    public class NavigateComand : CommandBase
    {
        private AdminViewModel _adminViewModel;
        private string sign;
        public NavigateComand(AdminViewModel adminViewModel, string sign)
        {
            this._adminViewModel = adminViewModel;
            this.sign = sign;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (sign)
            {
                case "dashboard":
                    _adminViewModel.CurrentViewModel = new AdminDashboardViewModel();
                    return;
                case "product":
                    _adminViewModel.CurrentViewModel = new AdminProductViewModel();
                    return;
                case "supply":
                    _adminViewModel.CurrentViewModel = new AdminSupplyViewModel();
                    return;
            }
        }
    }
}
