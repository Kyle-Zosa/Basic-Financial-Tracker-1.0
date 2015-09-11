using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Financial_Tracker
{
    public class Transactions
    {
        public const string table = "TransactionTable";
        public const string transactionID = "TRANSACTION_ID";
        public const string dateCreated = "DATE_CREATED";
        public const string amount = "AMOUNT";
        public const string type = "TRANSACTION_TYPE";
        public const string category = "CATEGORY";
        public const string description = "DESCRIPTION";
        public const string dateModified = "DATE_MODIFIED";

        public const string revenue = "Revenue";
        public const string expense = "Expense";

        public static List<string> getTransactionsColumns()
        {
            List<string> transactionsColumns = new List<string>(new string[] { transactionID, dateCreated, amount, type, category, description, dateModified });
            return transactionsColumns;
        }
        public static string getTransactionType(string inputAmount)
        {
            double double_inputAmount;
            double.TryParse(inputAmount, out double_inputAmount);
            if (double_inputAmount >= 0) return "Revenue";
            else return "Expense";
        }
        public static string getTransactionType(double inputAmount)
        {
            if (inputAmount >= 0) return "Revenue";
            else return "Expense";
        }
    }
}
