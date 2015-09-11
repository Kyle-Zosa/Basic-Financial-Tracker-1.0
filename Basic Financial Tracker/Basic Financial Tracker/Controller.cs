using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Basic_Financial_Tracker
{
    public class Controller
    {
        MainView view;
        Model model;

        public Controller(MainView view, Model model)
        {
            this.view = view;
            this.model = model;
        }
        public void populateTranasctionDataGrid()
        {
            this.view.transactionDataGrid.DataContext = this.model.populateDataGrid(Queries.transactionGridQuery, Bindings.transactionGrid, Transactions.table);
        }
        public DataSet populateEditHistoryDataGrid(HistoryWindow editHistory, string transactionID)
        {
            string query = Queries.modificationGridQuery + " WHERE " + Transactions.transactionID + " = '" + transactionID + "'";
            return this.model.populateDataGrid(query, Bindings.editGrid, Modifications.table);
        }
        public void refreshView()
        {
            this.view.currentBalanceValue.Content = "P " + calculateCurrentBalance();
            this.view.originalBalance.Content = "Original Balance: " + getOriginalBalance();
            this.view.todayInFlowValue.Content = "P " + getTodayRevenue();
            this.view.todayOutFlowValue.Content = "P " + getTodayExpense();
            this.view.todayNetFlowValue.Content = "P " + getTodayNet();

            updateAddMenuTransactionID();
            populateTranasctionDataGrid();
        }
        public string getTodayRevenue()
        {
            string revenue = ValidationConstants.emptyString;
            string condition = Transactions.dateCreated + " = '" + DateTime.Today.ToString("M/dd/yyyy") + "'";
            condition += " AND " + Transactions.type + " = '" + Transactions.revenue + "'";

            revenue = this.model.sumColumn(Transactions.table, Transactions.amount, condition)[0];
            return (revenue == "") ? "0.00" : String.Format("{0:0.00}", Convert.ToDouble(revenue));
        }

        public List<string> rowsToList(IList selectedItems)
        {
            List<string> rowList = new List<string>();
            foreach (DataRowView dataRow in selectedItems)
            {
                rowList.Add(dataRow.ToString());
            }
            return rowList;
        }

        public DateTime validateDate(string dateString)
        {
            DateTime validDate = DateTime.ParseExact(dateString, "M/dd/yyyy", CultureInfo.InvariantCulture);
            return validDate;
        }

        public bool isNumeric(string text)
        {
            double temp;
            bool status = double.TryParse(text, out temp);
            return status;
        }

        public string getTodayExpense()
        {
            string expense = ValidationConstants.emptyString;
            string condition = Transactions.dateCreated + " = '" + DateTime.Today.ToString("M/dd/yyyy") + "'";
            condition += " AND " + Transactions.type + " = '" + Transactions.expense + "'";

            expense = this.model.sumColumn(Transactions.table, Transactions.amount, condition)[0];
            return (expense == "") ? "0.00" : String.Format("{0:0.00}", Convert.ToDouble(expense));
        }

        public void deleteTransaction(string transactionID)
        {
            this.model.delete(Transactions.table, Transactions.transactionID, transactionID);
        }

        public string getTodayNet()
        {
            double net = Convert.ToDouble(getTodayRevenue()) + Convert.ToDouble(getTodayExpense());
            return String.Format("{0:0.00}", net);
        }
        public string calculateCurrentBalance()
        {
            double originalBalance = Convert.ToDouble(getOriginalBalance());
            double totalBalance = originalBalance;
            List<string> transactionAmountList = this.model.selectSpecificColumn(Transactions.table, Transactions.amount);

            foreach (string amount in transactionAmountList)
            {
                if (isNumeric(amount))
                {
                    totalBalance = totalBalance + Convert.ToDouble(amount);
                }
            }
            string currentBalance = String.Format("{0:0.00}", totalBalance);
            return commifyDecimal(currentBalance);
        }
        public void updateCurrentBalance()
        {
            string currentBalance = decommifyDecimal(calculateCurrentBalance());
            updateCurrentBalance(currentBalance);
        }
        public void updateCurrentBalance(string currentBalance)
        {
            this.model.update(Constants.table, Constants.name, Constants.currentBalance, Constants.value, currentBalance);
        }

        public void updateEntry(string table, string transactionID, string value, List<string> updateColumns, List<string> updateValues)
        {
            this.model.update(table, transactionID, value, updateColumns, updateValues);
        }

        public string getOriginalBalance()
        {
            string originalBalance = this.model.selectSpecificValue(Constants.table, Constants.value, Constants.name, Constants.originalBalance);
            return commifyDecimal(originalBalance);
        }
        public void updateOriginalBalance(string originalBalance)
        {
            this.model.update(Constants.table, Constants.name, Constants.originalBalance, Constants.value, originalBalance);
        }

        public void addTo(string table, List<string> modificationsColumns, List<string> modificationValues)
        {
            this.model.insert(table, modificationsColumns, modificationValues);
        }

        public void updateAddMenuTransactionID()
        {
            int nextID = (this.model.largestValue(Transactions.table, Transactions.transactionID) + 1);
            this.view.transactionIDTextBox.Text = nextID.ToString();
        }

        public string commifyDecimal(string decimalNumber)
        {
            if (isNumeric(decimalNumber))
            {
                double number;
                double.TryParse(decimalNumber, out number);
                return Convert.ToDecimal(number).ToString("#,##0.00");
            }
            return ValidationConstants.emptyString;
        }
        public string commifyDecimal(double decimalNumber)
        {
            return String.Format("{#,##0.00}", decimalNumber);
        }
        public string decommifyDecimal(string decimalNumber)
        {
            return decimalNumber.Replace(",", "");
        }
        public string formatTwoDecimalPlaces(double decimalNumber)
        {
            return String.Format("{0:0.00}", decimalNumber);
        }
    }
}