using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using DevCoffeeManagerApp.DAOs;
using System;

namespace DevCoffeeManagerApp.Commands.CommandTable
{
    public class CommandSubmitTable : CommandBase
    {
        private TableDAO tableDAO = new TableDAO();
        private NavigationStore _navigationStore;
        private TableViewModel tableViewModel;
        public CommandSubmitTable(TableViewModel viewmodeltable, NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
            this.tableViewModel = viewmodeltable;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ListView listView)
            {
                MessageBox.Show("Đặt bàn thành công");
                // Lấy danh sách các mục đã chọn từ SelectedItems               
                ObservableCollection<TableModel> Items = new ObservableCollection<TableModel>(listView.SelectedItems.Cast<TableModel>());
                var filteritem = tableDAO.ReadAll().Where(item => !item.Status.Value).Select(item => item.No_).ToList();
                var tablebooked = Items.Where(item => !filteritem.Contains(item.No_)).ToList();
                var tablereset = tableViewModel.Items.Where(item => item.Status.Value && filteritem.Contains(item.No_)).ToList();
                foreach (var item in tablereset)
                {
                    tableDAO.SetStatus(item.No_);
                }
                SessionStatic.SetTables = new ObservableCollection<TableModel>(tablebooked);
                _navigationStore.CurrentViewModel = new OrderViewModel(_navigationStore);
            }
        }
    }
}
