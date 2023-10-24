using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.newe
{
    public class CommandBookTable: CommandBase
    {
        public CommandBookTable() { }
        TableDAO tableDAO = new TableDAO();
        private TableViewModel Viewmodeltable;
        public CommandBookTable(TableViewModel viewmodeltable)
        {
            Viewmodeltable = viewmodeltable;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            foreach (var item in Viewmodeltable.Items)
            {
                // lấy đa ta sử lý ở đây nhé
            }
        }

    }
}
