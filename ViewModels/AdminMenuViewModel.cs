using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminMenuViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }

        public AdminMenuViewModel()
        {
            Date = DateTime.Now;

        }
    }
}
