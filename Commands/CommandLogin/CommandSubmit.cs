using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
namespace DevCoffeeManagerApp.Commands.CommandLogin
{
    public class CommandSubmit : CommandBase
    {
        StaffDAO staffdao = new StaffDAO();
        private LoginViewModel Viewmodellogin;
        private ObservableCollection<string> Items { get; set; }
        public CommandSubmit(LoginViewModel viewmodellogin, ObservableCollection<string> items)
        {
            Viewmodellogin = viewmodellogin;
            Items = items;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            MessageBox.Show(staffdao.ReadAll()[0].phone_staff.ToString());
        }
    }
}
