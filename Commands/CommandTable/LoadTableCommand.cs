using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandLogin
{
    public class LoadTableCommand : CommandBase
    {
        TableDAO tableDAO = new TableDAO();    
        private TableViewModel Viewmodeltable;
        public LoadTableCommand(TableViewModel viewmodeltable)
        {
            Viewmodeltable = viewmodeltable;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            getAll();
        }
        public void getAll()
        {
            Viewmodeltable.Items = tableDAO.ReadAll();
        }

    }

}
