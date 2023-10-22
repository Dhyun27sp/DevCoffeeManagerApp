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
    public class ReservationCommand : CommandBase
    {
        TableDAO tableDAO = new TableDAO();
        private TableViewModel Viewmodeltable;
        public ReservationCommand(TableViewModel viewmodeltable)
        {
            Viewmodeltable = viewmodeltable;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Reservation(Viewmodeltable.Items);
        }
        public void Reservation(List<TableModel> tables)
        {
            foreach (var i in tables)
            {
                tableDAO.SetStatus(i.No_);
            }
        }
    }

}
