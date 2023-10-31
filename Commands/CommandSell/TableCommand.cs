using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandMainStaff
{
    public class TableCommand : CommandBase
    {
        TableDAO tableDAO = new TableDAO();
        TableViewModel tableViewModel = new TableViewModel();
        private SellViewModel SellViewModel;
        public TableCommand (SellViewModel SellViewModel)
        {
            this.SellViewModel = SellViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            SellViewModel.CurrentViewModel = tableViewModel;
        }
    }
}
