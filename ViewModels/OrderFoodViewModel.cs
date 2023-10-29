using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using DevCoffeeManagerApp.DAOs;
using System.Windows;

namespace DevCoffeeManagerApp.ViewModels
{
    public class OrderFoodViewModel
    {
        MenuDAO menuDao = new MenuDAO();
        public List<DishModel> Dishs { get; set; }
        public OrderFoodViewModel() 
        {
            Dishs = menuDao.ReadAll("Coffee").dish;
        }
    }
}
