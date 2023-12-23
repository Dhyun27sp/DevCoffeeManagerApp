using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminDashboardViewModel:BaseViewModel
    {
        public DateTime Date { get; set; }

        public AdminDashboardViewModel() 
        {
            Date = DateTime.Now;
        }
    }
}
