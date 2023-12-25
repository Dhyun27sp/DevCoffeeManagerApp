using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.Commands.CommandAdminReceipt
{
    public class FilterReceiptCommand : CommandBase
    {
        private AdminReceiptViewModel viewModel;
        private string action;
        ReceiptDAO receiptDAO = new ReceiptDAO();
        public FilterReceiptCommand(AdminReceiptViewModel viewModel, string action)
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
                case "search":
                    Search();
                    return;
            }
        }
        public void Search()
        {
            if(viewModel.ReceiptDate == null)
            {
                ObservableCollection<ReceiptModel> listAll = new ObservableCollection<ReceiptModel>();
                listAll = new ObservableCollection<ReceiptModel>(receiptDAO.ReadAll());
                if(!string.IsNullOrWhiteSpace(viewModel.ReceiptSearch))
                {
                    ObservableCollection<ReceiptModel> listSearch = new ObservableCollection<ReceiptModel>();
                    foreach (ReceiptModel receipt in listAll)
                    {
                        if (receipt.receipt_code.Replace(" ", "").IndexOf(viewModel.ReceiptSearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            listSearch.Add(receipt);
                        }
                    }
                    viewModel.Receipts = listSearch;
                }
                else
                {
                    viewModel.Receipts = listAll;
                }

            }
            else
            {
                ObservableCollection<ReceiptModel> listByDate = new ObservableCollection<ReceiptModel>();
                listByDate = new ObservableCollection<ReceiptModel>(receiptDAO.FindReceiptOnDate(viewModel.ReceiptDate.Value));
                if (!string.IsNullOrWhiteSpace(viewModel.ReceiptSearch))
                {
                    ObservableCollection<ReceiptModel> listSearch = new ObservableCollection<ReceiptModel>();
                    foreach (ReceiptModel receipt in listByDate)
                    {
                        if (receipt.receipt_code.Replace(" ", "").IndexOf(viewModel.ReceiptSearch.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            listSearch.Add(receipt);
                        }
                    }
                    viewModel.Receipts = listSearch;
                }
                else
                {
                    viewModel.Receipts = listByDate;
                }
            }
        }
    }
}
