using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Financial_Tracker
{
    public class Modifications
    {
        public const string table = "ModificationTable";
        public const string modificationID = "MODIFICAION_ID";
        public const string transactionID = "TRANSACTION_ID";
        public const string originalAmount = "ORIGINAL_AMOUNT";
        public const string originalCategory = "ORIGINAL_CATEGORY";
        public const string originalDescription = "ORIGINAL_DESCRIPTION";
        public const string newAmount = "NEW_AMOUNT";
        public const string newCategory = "NEW_CATEGORY";
        public const string newDescription = "NEW_DESCRIPTION";
        public const string currentDate = "CURRENT_DATE";
        public const string previousDate = "PREVIOUS_DATE";
        public const string reasonForChange = "REASON_FOR_CHANGE";

        public static List<string> getModificationsColumns()
        {
            List<string> modificationsColumns = new List<string>(new string[] { modificationID, transactionID, originalAmount, originalCategory, originalDescription, newAmount, newCategory, newDescription, currentDate, previousDate, reasonForChange });
            return modificationsColumns;
        }
    }
}
