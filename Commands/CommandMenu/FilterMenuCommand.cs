using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandMenu
{
    public class FilterMenuCommand:CommandBase
    {
        private AdminMenuViewModel viewmodel;
        private string action;
        MenuDAO menuDAO = new MenuDAO();
        ProductDAO productDAO = new ProductDAO();
        public FilterMenuCommand(AdminMenuViewModel viewmodel, string action)
        {
            this.viewmodel = viewmodel;
            this.action = action;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (action)
            {
                case "cbb":
                    CombbChange(parameter);
                    return;
            }
        }
        private void CombbChange(object parameter) {
            if (viewmodel.Type == "All")
            {
                viewmodel.Dishes = new ObservableCollection<DishModel>(viewmodel.LoadAllDish());
            }
            else
            {
                LoadTypeDish();
            }
        }

        private void LoadTypeDish()
        {
            ObservableCollection < DishModel > dishs = new ObservableCollection<DishModel> ();
            foreach (DishModel d in menuDAO.ReadOnetype(viewmodel.Type).dish)
            {
                d.category = viewmodel.Type;
                dishs.Add(d);
            }
            viewmodel.Dishes = dishs;
        }
    }
}
