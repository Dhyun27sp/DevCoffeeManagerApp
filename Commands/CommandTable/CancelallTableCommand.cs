using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandTable
{
    public class CancelallTableCommand : CommandBase
    {
        TableViewModel viewmodeltable;
        public CancelallTableCommand(TableViewModel viewmodeltable)
        {
            this.viewmodeltable = viewmodeltable;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {

            if (parameter is ListView listView)
            {
                if (listView.SelectedItems.Count > 0)
                {
                    listView.SelectedItems.Clear();
                }
            }
                foreach (TableModel item in viewmodeltable.Items)
            {
                item.Status = true;
            }
            viewmodeltable.Items = viewmodeltable.Items;
            SessionStatic.SetTables = null;
            MessageBox.Show("Trả bàn thành công!");
        }
    }
}
