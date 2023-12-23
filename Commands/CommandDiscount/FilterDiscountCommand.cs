using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.AdminCommand.DiscountCommands
{
    public class FilterDiscountCommand: CommandBase
    {
        public FilterDiscountCommand() { }

        private AdminDiscountViewModel viewModel;
        private string action;
        MenuDAO menuDao = new MenuDAO();
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
            switch (action)
            {
                case "tyledishchange":
                    changetypedish(viewModel.DiscountID);
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

        private void changetypedish(string discountid)
        {
            if(discountid != null)
            {
                DiscountModel discountchoosed = discountDAO.findisbyId(discountid);
                ObservableCollection<DishModel> listdish = new ObservableCollection<DishModel>();
                if (viewModel.TypeDishitem == "All Dishs")
                {
                    listdish = new ObservableCollection<DishModel>(menuDao.ReadAll_Dish());
                }
                else
                {
                    listdish = new ObservableCollection<DishModel>(menuDao.findMenubyname(viewModel.TypeDishitem).dish);
                }
                viewModel.ListDishs = new ObservableCollection<DishModel>();
                int insite_one = 0;
                foreach (DishModel dishdc in discountchoosed.apply.dish)
                {
                    foreach (DishModel dish in listdish)
                    {
                        if (dishdc._id == dish._id)
                        {
                            viewModel.ListDishs.Add(dish);
                            insite_one = 1;
                            break;
                        }
                    }
                }
                if (insite_one == 1)
                {
                    viewModel.ListDishs = viewModel.ListDishs;
                }
                else
                {
                    viewModel.ListDishs = new ObservableCollection<DishModel>();
                }
                var result = listdish.Where(cm => !viewModel.ListDishs.Any(cma => cma._id == cm._id)).ToList();
                viewModel.ListDishsNotDC = new ObservableCollection<DishModel>(result);
            }
            else
            {
                MessageBox.Show("Chưa có giảm giá được chọn");
            }
        }
    }
}
