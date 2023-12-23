using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminProductViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }

        public AdminProductViewModel() 
        {
            Date = DateTime.Now;
        }    
    }
}
