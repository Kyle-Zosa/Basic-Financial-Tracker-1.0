using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Financial_Tracker
{
    public class Queries
    {
        public const string transactionGridQuery = "SELECT TRANSACTION_ID AS 'Transaction ID', DATE_CREATED AS 'Date Created', AMOUNT AS 'Amount', TRANSACTION_TYPE AS 'Type', CATEGORY AS 'Category', DESCRIPTION AS 'Description', DATE_MODIFIED AS 'Last Modified' FROM TransactionTable";
        public const string modificationGridQuery = "SELECT MODIFICATION_ID AS 'Modification ID', ORIGINAL_AMOUNT AS 'Original Amount', ORIGINAL_CATEGORY AS 'Original Category', ORIGINAL_DESCRIPTION AS 'Original Description', NEW_AMOUNT AS 'New Amount', NEW_CATEGORY AS 'New Category', NEW_DESCRIPTION AS 'New Description', CURRENT_DATE AS 'Date Edited', PREVIOUS_DATE AS 'Last Edit', REASON_FOR_CHANGE AS 'Reason for Change' FROM ModificationsTable";
    }
}
