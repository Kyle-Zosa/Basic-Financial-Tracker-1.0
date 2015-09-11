using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Financial_Tracker
{
    public class Constants
    {
        public const string table = "Constants";
        public const string name = "NAME";
        public const string dataType = "DATA_TYPE";
        public const string value = "VALUE";

        public const string originalBalance = "ORIGINAL BALANCE";
        public const string currentBalance = "CURRENT BALANCE";

        public static List<string> getConstantsColumns()
        {
            List<string> constantsColumns = new List<string>(new string[] { name, dataType, value });
            return constantsColumns;
        }
    }
}
