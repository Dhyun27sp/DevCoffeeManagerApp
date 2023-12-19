using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.AdminCommand.DiscountCommands
{
    public class FilterDiscountCommand: CommandBase
    {
        public FilterDiscountCommand() { }

        private AdminDiscountViewModel viewModel;
        private string action;
        DiscountDAO discountDAO = new DiscountDAO();
        public FilterDiscountCommand(AdminDiscountViewModel viewModel, string action)
        {
            this.action = action;
            this.viewModel = viewModel;
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
                    combbdiscount();
                    return;
                case "search":
                    return;
            }
        }
        private void combbdiscount() {
            ObservableCollection<DiscountModel> TypeDiscount = new ObservableCollection<DiscountModel>();
            TypeDiscount = new ObservableCollection<DiscountModel>(discountDAO.ReadDiscountAll());
            if (viewModel.ExpiryDateitem == "All")
            {
                viewModel.Discounts = TypeDiscount;
            }
            else if (viewModel.ExpiryDateitem == "Expired")
            {
                ObservableCollection<DiscountModel> ExpiredDiscount = new ObservableCollection<DiscountModel>();
                foreach(DiscountModel discount in TypeDiscount)
                {
                    if(discount.dayend < DateTime.UtcNow)
                    {
                        ExpiredDiscount.Add(discount);
                    }
                }
                viewModel.Discounts = ExpiredDiscount;
            }
            else if(viewModel.ExpiryDateitem == "valid")
            {
                ObservableCollection<DiscountModel> ExpiredDiscount = new ObservableCollection<DiscountModel>();
                foreach (DiscountModel discount in TypeDiscount)
                {
                    if (discount.dayend >= DateTime.UtcNow)
                    {
                        ExpiredDiscount.Add(discount);
                    }
                }
                viewModel.Discounts = ExpiredDiscount;
            }
        }
    }
}
