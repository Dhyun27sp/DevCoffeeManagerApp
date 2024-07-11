using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.AdminCommand.DiscountCommands
{
    public class ClickButtonCommand : CommandBase
    {
        private AdminDiscountViewModel viewModel;
        private string action;
        DiscountDAO discountDAO = new DiscountDAO();
        MenuDAO menuDao = new MenuDAO();
        public ClickButtonCommand(AdminDiscountViewModel viewModel, string action)
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
                case "choose":
                    chooseDiscount(parameter);
                    return;
                case "add":
                    addDiscount(parameter);
                    return;
                case "deletef":
                    DeletefieldDiscount(parameter);
                    return;
                case "update":
                    UpdateDistcount(parameter);
                    return;
                case "deleted":
                    DeleteDiscount(parameter);
                    return;
                case "choosedishremove":
                    choosedishapplytoremove(parameter);
                    return;
                case "choosedishtoadd":
                    choosedishapplytoadd(parameter);
                    return;
                case "choosemenutoremove":
                    choosemenuapplytoremove(parameter);
                    return;
                case "choosemenutoadd":
                    choosemenuapplytoadd(parameter);
                    return;
            }
        }

        private void chooseDiscount(object parameter)
        {
            if (parameter is ListView listdiscount)
            {
                if (listdiscount.SelectedItem != null)
                {
                    Tuple<DiscountModel, int> selectedItem = (Tuple<DiscountModel, int>)listdiscount.SelectedItem;
                    viewModel.NotallowedAdd = true;
                    viewModel.DiscountID = selectedItem.Item1.discountid;
                    viewModel.Discountname = selectedItem.Item1.name;
                    viewModel.Description = selectedItem.Item1.detail;
                    viewModel.Daystart = selectedItem.Item1.daystart;
                    viewModel.Dayend = selectedItem.Item1.dayend;
                    viewModel.Value = selectedItem.Item1.value_dis;
                    applydish(selectedItem.Item1.discountid);
                    applymenu(selectedItem.Item1.discountid);
                }
            }
        }
        private void addDiscount(object parameter)
        {
            if (viewModel.NotallowedAdd == true)
            {
                MessageBox.Show("Khuyến mã tồn tại");
            }
            else
            {
                if (discountDAO.findisbyId(viewModel.DiscountID) != null)
                {
                    MessageBox.Show("Khuyến mã tồn tại");
                }
                else
                {
                    if (checkedinput() == true)
                    {
                        ApplyModel applyModel = new ApplyModel();
                        List<MenuModel> menus = new List<MenuModel>();
                        List<DishModel> dishs = new List<DishModel>();
                        applyModel.menu = menus;
                        applyModel.dish = dishs;
                        DiscountModel newdiscount = new DiscountModel(viewModel.DiscountID, viewModel.Discountname, viewModel.Description, applyModel, viewModel.Daystart, viewModel.Dayend, viewModel.Value);
                        discountDAO.createdistcount(newdiscount);
                        MessageBox.Show("Thêm Khuyến mãi thành công");
                        viewModel.Discounts.Add(newdiscount);
                        viewModel.Discounts = viewModel.Discounts;
                    }
                    else
                    {
                        MessageBox.Show("Thông tin Không hợp lệ");
                    }
                }
            }
        }
        private bool checkedinput()
        {
            if (string.IsNullOrWhiteSpace(viewModel.DiscountID))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(viewModel.Discountname))
            {
                return false;
            }
            else if (viewModel.Daystart == null)
            {
                return false;
            }
            else if (viewModel.Dayend == null)
            {
                return false;
            }
            else if (viewModel.Value == null)
            {
                return false;
            }
            return true;
        }
        private void DeletefieldDiscount(object parameter)
        {
            viewModel.NotallowedAdd = false;
            MessageBox.Show("Đã xoá khuyến mãi");
            viewModel.DiscountID = "";
            viewModel.Discountname = "";
            viewModel.Description = "";
            viewModel.Daystart = DateTime.Now;
            viewModel.Dayend = DateTime.Now;
            viewModel.Value = "";

        }
        private void UpdateDistcount(object parameter)
        {
            DiscountModel discountModel = discountDAO.findisbyId(viewModel.DiscountID);
            if (discountModel != null)
            {
                ApplyModel applyModel = new ApplyModel();
                List<DishModel> listdish = new List<DishModel>(viewModel.ListDishs);
                List<MenuModel> listmenu = new List<MenuModel>(viewModel.ListMenu);

                applyModel.dish = DeepCloneListDish(listdish);
                applyModel.menu = DeepCloneListMenu(listmenu);
                DiscountModel newdiscount = new DiscountModel(viewModel.DiscountID, viewModel.Discountname, viewModel.Description, applyModel, viewModel.Daystart, viewModel.Dayend, viewModel.Value);
                Console.WriteLine(viewModel.Daystart + " " + viewModel.Dayend);
                discountDAO.UpdateDiscount(newdiscount);
                Console.WriteLine(newdiscount.daystart + " " + newdiscount.dayend);
                MessageBox.Show("Cập nhật thành công");
                viewModel.Discounts = new ObservableCollection<DiscountModel>(discountDAO.ReadDiscountAll());
            }
            else
            {
                MessageBox.Show("Ko thể cập nhật khi vì discount ko tồn tại");
            }
        }
        private void DeleteDiscount(object parameter)
        {
            if (parameter is DiscountModel discount)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    discountDAO.DeleteDiscountBydiscountid(discount.discountid);
                    MessageBox.Show("Xóa thành công");
                    viewModel.Discounts.Remove(discount);
                    viewModel.Discounts = viewModel.Discounts;
                }
            }
        }
        private void applydish(string discountid)
        {
            DiscountModel discountchoosed = discountDAO.findisbyId(discountid);
            ObservableCollection<DishModel> listdish = new ObservableCollection<DishModel>();
            viewModel.TypeDishitem = "All Dishs";
            listdish = new ObservableCollection<DishModel>(menuDao.ReadAll_Dish());
            viewModel.ListDishs = new ObservableCollection<DishModel>();
            if (discountchoosed.apply.dish.Count > 0)
            {
                foreach (DishModel dishdc in discountchoosed.apply.dish)
                {
                    foreach (DishModel dish in listdish)
                    {
                        if (dishdc._id == dish._id)
                        {
                            viewModel.ListDishs.Add(dish);
                            break;
                        }
                    }
                }
                viewModel.Flag = false;
            }
            else
            {
                viewModel.Flag = true;
            }
            viewModel.ListDishs = viewModel.ListDishs;
            var result = listdish.Where(cm => !viewModel.ListDishs.Any(cma => cma._id == cm._id)).ToList();
            viewModel.ListDishsNotDC = new ObservableCollection<DishModel>(result);
        }

        private void applymenu(string discountid)
        {
            DiscountModel discountchoosed = discountDAO.findisbyId(discountid);
            ObservableCollection<MenuModel> listmenu = new ObservableCollection<MenuModel>(menuDao.ReadAll_Type_dish());
            viewModel.ListMenu = new ObservableCollection<MenuModel>();
            if (discountchoosed.apply.menu.Count > 0)
            {
                foreach (MenuModel menudc in discountchoosed.apply.menu)
                {
                    foreach (MenuModel menu in listmenu)
                    {
                        if (menudc.id == menu.id)
                        {
                            viewModel.ListMenu.Add(menu);
                            break;
                        }
                    }
                }
                viewModel.Flag = true;
            }
            else
            {
                viewModel.Flag = false;
            }
            viewModel.ListMenu = viewModel.ListMenu;
            var result = listmenu.Where(cm => !viewModel.ListMenu.Any(cma => cma.id == cm.id)).ToList();
            viewModel.ListMenuNotDC = new ObservableCollection<MenuModel>(result);
        }
        private void choosedishapplytoremove(object parameter)
        {
            if (parameter is ListView listdish)
            {
                if (listdish.SelectedItem != null)
                {
                    Tuple<DishModel, int> selectedItem = (Tuple<DishModel, int>)listdish.SelectedItem;
                    string DiscountID = viewModel.DiscountID;
                    DishModel dish = new DishModel();
                    dish = selectedItem.Item1;
                    discountDAO.RemoveDishFromDiscount(DiscountID, dish);
                    MessageBox.Show("Xóa Món Trong danh sách thành công");
                    viewModel.ListDishs.Remove(selectedItem.Item1);
                    viewModel.ListDishsNotDC.Add(selectedItem.Item1);
                    viewModel.ListDishs = viewModel.ListDishs;
                    viewModel.ListDishsNotDC = viewModel.ListDishsNotDC;
                }
            }
        }
        private void choosedishapplytoadd(object parameter)
        {
            if (parameter is ListView listdish)
            {
                if (listdish.SelectedItem != null)
                {
                    Tuple<DishModel, int> selectedItem = (Tuple<DishModel, int>)listdish.SelectedItem;
                    DishModel copydish = DeepClone(selectedItem.Item1);
                    discountDAO.AddDishToDiscount(viewModel.DiscountID, copydish);
                    MessageBox.Show("Thêm Món vào Giảm giá thành công");
                    viewModel.ListDishs.Add(selectedItem.Item1);
                    viewModel.ListDishsNotDC.Remove(selectedItem.Item1);
                    viewModel.ListDishs = viewModel.ListDishs;
                    viewModel.ListDishsNotDC = viewModel.ListDishsNotDC;
                    if (viewModel.ListMenu.Count > 0)
                    {
                        viewModel.ListMenu = new ObservableCollection<MenuModel>();
                        viewModel.ListMenuNotDC = new ObservableCollection<MenuModel>(menuDao.ReadAll_Type_dish());
                    }
                }
            }
        }
        public static DishModel DeepClone(DishModel source)
        {
            if (source == null)
                return null;

            DishModel target = new DishModel
            {
                _id = source._id,
                dish_name = source.dish_name,
                ingredient = new List<ProductModel>(source.ingredient),
                price = source.price,
                image = source.image,
                date_add = source.date_add,
                Saleprice = source.price
            };

            return target;
        }
        private void choosemenuapplytoremove(object parameter)
        {
            if (parameter is ListView listmenu)
            {
                if (listmenu.SelectedItem != null)
                {
                    Tuple<MenuModel, int> selectedItem = (Tuple<MenuModel, int>)listmenu.SelectedItem;
                    discountDAO.RemoveMenuFromDiscount(viewModel.DiscountID, selectedItem.Item1);
                    MessageBox.Show("Xóa Loại món thành công");
                    viewModel.ListMenu.Remove(selectedItem.Item1);
                    viewModel.ListMenuNotDC.Add(selectedItem.Item1);
                    viewModel.ListMenu = viewModel.ListMenu;
                    viewModel.ListMenuNotDC = viewModel.ListMenuNotDC;
                }
            }
        }
        private void choosemenuapplytoadd(object parameter)
        {
            if (parameter is ListView listmenu)
            {
                if (listmenu.SelectedItem != null)
                {
                    Tuple<MenuModel, int> selectedItem = (Tuple<MenuModel, int>)listmenu.SelectedItem;
                    MenuModel copymenu = DeepCloneMenu(selectedItem.Item1);
                    discountDAO.AddMenuToDiscount(viewModel.DiscountID, copymenu);
                    MessageBox.Show("Thêm Loại món thành công");
                    viewModel.ListMenu.Add(selectedItem.Item1);
                    viewModel.ListMenuNotDC.Remove(selectedItem.Item1);
                    viewModel.ListMenu = viewModel.ListMenu;
                    viewModel.ListMenuNotDC = viewModel.ListMenuNotDC;
                    if (viewModel.ListDishs.Count > 0)
                    {
                        viewModel.ListDishs = new ObservableCollection<DishModel>();
                        viewModel.ListDishsNotDC = new ObservableCollection<DishModel>(menuDao.ReadAll_Dish());
                    }
                }
            }
        }
        public static MenuModel DeepCloneMenu(MenuModel source)
        {
            if (source == null)
                return null;

            MenuModel target = new MenuModel
            {
                id = source.id,
                type_of_dish = source.type_of_dish,
                detail = source.detail,
                dish = new List<DishModel>(source.dish),

            };

            return target;
        }
        private static List<MenuModel> DeepCloneListMenu(List<MenuModel> listmenu)
        {
            List<MenuModel> listmenucopy = new List<MenuModel>();
            foreach (MenuModel menu in listmenu)
            {
                MenuModel copymenu = new MenuModel();
                copymenu = DeepCloneMenu(menu);
                copymenu.dish = new List<DishModel>();
                copymenu.type_of_dish = null;
                copymenu.detail = null;
                listmenucopy.Add(copymenu);
            }
            return listmenucopy;
        }
        private static List<DishModel> DeepCloneListDish(List<DishModel> listdish)
        {
            List<DishModel> listdishcopy = new List<DishModel>();
            foreach (DishModel dish in listdish)
            {
                DishModel copydish = new DishModel();
                copydish = DeepClone(dish);
                copydish.dish_name = null;
                copydish.ingredient = null;
                copydish.price = null;
                copydish.image = null;
                copydish.date_add = null;
                copydish.Saleprice = null;
                listdishcopy.Add(copydish);
            }
            return listdishcopy;
        }
    }
}
