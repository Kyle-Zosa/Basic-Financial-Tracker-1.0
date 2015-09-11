using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Data;

namespace Basic_Financial_Tracker
{
    public class Model
    {
        private SQLiteConnection connection;
        private string connectionString = "Data Source = BasicFinancialTracker.db;Version=3";
        public Model()
        {
            this.connection = new SQLiteConnection(this.connectionString);
            this.openConnection();
            this.closeConnection();
        }
        #region Connection Functions
        public bool openConnection()
        {
            try
            {
                this.connection.Open();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            return false;
        }
        public bool closeConnection()
        {
            try
            {
                this.connection.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            return false;
        }
        #endregion
        #region Queries
        #region Select Queries
        public Dictionary<string, string> selectAll(string table)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string selectQuery = "SELECT * FROM " + table;

            this.openConnection();
            try
            {
                SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, this.connection);
                SQLiteDataReader dataReader = selectCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    data.Add(dataReader.GetString(0), dataReader.GetString(1));
                    MessageBox.Show(dataReader.GetString(0) + " and " + dataReader.GetString(1));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }

            this.closeConnection();
            return data;
        }
        public Dictionary<string, string> selectWhere(string table, List<string> columns, List<string> values)
        {
            if (columns.Count != values.Count) return null;

            Dictionary<string, string> data = new Dictionary<string, string>();
            string selectQuery = "SELECT * FROM " + table + "WHERE";

            for (int i = 0; i < columns.Count; i++)
            {
                selectQuery = selectQuery + " " + columns[i] + "=" + values[i];
            }

            this.openConnection();
            try
            {
                SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, this.connection);
                SQLiteDataReader dataReader = selectCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    data.Add(dataReader.GetString(0), dataReader.GetString(1));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }

            this.closeConnection();
            return data;
        }

        public List<string> sumColumn(string table, string column, string condition)
        {
            string sumQuery = "SELECT SUM(" + column + ") AS SUM FROM " + table + " WHERE " + condition;
            List<string> columnOfSums = new List<string>();
            this.openConnection();
            try
            {
                Console.WriteLine(sumQuery);
                SQLiteCommand selectCommand = new SQLiteCommand(sumQuery, this.connection);
                SQLiteDataReader dataReader = selectCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    columnOfSums.Add(Convert.ToString(dataReader["SUM"]));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }

            this.closeConnection();
            return columnOfSums;
        }
        public string selectSpecificValue(string table, string selectColumn, string conditionColumn, string conditionValue)
        {
            string data = ValidationConstants.emptyString;
            string selectQuery = "SELECT " + selectColumn + " FROM " + table + " WHERE " + conditionColumn + " = '" + conditionValue + "'";

            this.openConnection();
            try
            {
                SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, this.connection);
                SQLiteDataReader dataReader = selectCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    data = Convert.ToString(dataReader[selectColumn]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }

            this.closeConnection();
            return data;
        }
        public List<string> selectSpecificColumn(string table, string selectColumn)
        {
            List<string> data = new List<string>();
            string selectQuery = "SELECT " + selectColumn + " FROM " + table;

            this.openConnection();
            try
            {
                SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, this.connection);
                SQLiteDataReader dataReader = selectCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    data.Add(Convert.ToString(dataReader[selectColumn]));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }

            this.closeConnection();
            return data;
        }
        public DataSet populateDataGrid(string query, string databinding, string table)
        {
            DataSet tableData = new DataSet();
            string selectQuery = query;
            this.openConnection();
            Console.WriteLine(query);
            try
            {
                SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, this.connection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);
                adapter.Fill(tableData, databinding);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
            return tableData;
        }

        public DataSet populateDataGrid(string databinding, string table)
        {
            DataSet tableData = new DataSet();

            string selectQuery = "SELECT * FROM " + table;

            this.openConnection();
            try
            {
                SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, this.connection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);
                adapter.Fill(tableData, databinding);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
            return tableData;
        }
        public void populateDataGrid(string databinding, string table, List<string> columns, List<string> values)
        {
            if (columns.Count != values.Count) return;

            string selectQuery = "SELECT * FROM " + table + "WHERE";

            for (int i = 0; i < columns.Count; i++)
            {
                selectQuery = selectQuery + " " + columns[i] + "=" + values[i];
            }

            this.openConnection();
            try
            {
                SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, this.connection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);
                DataSet tableData = new DataSet();
                adapter.Fill(tableData, databinding);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }

            this.closeConnection();
        }
        #endregion Select Queries
        #region DB Manipulation Queries
        public void insert(string table, List<string> columns, List<string> values)
        {
            if (columns.Count != values.Count) return;

            string insertQuery = "INSERT INTO " + table + " (";
            string valuesQuery = "VALUES(";

            insertQuery += "'" + columns[0] + "'";
            valuesQuery += "'" + values[0] + "'";

            for (int i = 1; i < columns.Count; i++)
            {
                insertQuery += ",'" + columns[i] + "'";
                valuesQuery += ",'" + values[i] + "'";
            }

            insertQuery += ") ";
            valuesQuery += ")";

            insertQuery += valuesQuery;
            this.openConnection();
            try
            {
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, this.connection);
                insertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
        }
        public void update(string table, string conditionColumn, string conditionValue, List<string> columns, List<string> values)
        {
            if (columns.Count != values.Count) return;

            string updateQuery = "UPDATE " + table + " SET";

            updateQuery = updateQuery + " " + columns[0] + " = '" + values[0] + "'";
            for (int i = 1; i < columns.Count; i++)
            {
                updateQuery = updateQuery + ", " + columns[i] + " = '" + values[i] + "'";
            }

            updateQuery = updateQuery + " WHERE " + conditionColumn + " = '" + conditionValue + "'";
            this.openConnection();
            try
            {
                SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, this.connection);
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
        }
        public void update(string table, string conditionColumn, string conditionValue, string updateColumn, string updateValue)
        {
            string updateQuery = "UPDATE " + table + " SET " + updateColumn + " = '" + updateValue + "'";
            updateQuery = updateQuery + " WHERE " + conditionColumn + " = '" + conditionValue + "'";
            this.openConnection();
            try
            {
                SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, this.connection);
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
        }
        public void delete(string table, string column, string value)
        {
            string deleteQuery = "DELETE FROM " + table + " WHERE " + column + " = '" + value + "'";
            this.openConnection();
            try
            {
                SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, this.connection);
                deleteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
        }
        public int rowCount(string table)
        {
            int rowCount = 0;
            string countQuery = "SELECT COUNT(*) AS RowCount FROM " + table;
            this.openConnection();
            try
            {
                SQLiteCommand countCommand = new SQLiteCommand(countQuery, this.connection);
                SQLiteDataReader dataReader = countCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    rowCount = Convert.ToInt32(dataReader["RowCount"]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
            return rowCount;
        }
        public int largestValue(string table, string column)
        {
            string maxCount = "0";
            string maxQuery = "SELECT MAX(" + column + ") AS MAXVALUE FROM " + table;
            this.openConnection();
            try
            {
                SQLiteCommand maxCommand = new SQLiteCommand(maxQuery, this.connection);
                SQLiteDataReader dataReader;
                dataReader = maxCommand.ExecuteReader();

                while (dataReader.Read())
                {

                    maxCount = dataReader["MAXVALUE"].ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.StackTrace.ToString());
            }
            this.closeConnection();
            if (maxCount == "") maxCount = "0";
            return Convert.ToInt32(maxCount);
        }
        #endregion DB Manipulation Queries
        #endregion Queries
    }
}
