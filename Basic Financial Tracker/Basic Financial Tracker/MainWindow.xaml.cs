using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Collections;
using System.Data;

namespace Basic_Financial_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private Controller controller;
        private Model model;

        public string popupReturnButtonValue = ValidationConstants.emptyString;
        public string popupReturnValue = ValidationConstants.emptyString;
        public Dictionary<string, string> modifiedValues;

        public string defaultDescriptionText = "Type a short description here.";
        public string defaultAmountText = "0.00";
        public MainView()
        {
            InitializeComponent();

            MainWindow.Top = (SystemParameters.PrimaryScreenHeight / 2) - (MainWindow.Height / 2);
            MainWindow.Left = (SystemParameters.PrimaryScreenWidth / 2) - (MainWindow.Width / 2);

            this.model = new Model();
            this.controller = new Controller(this, this.model);
            this.controller.refreshView();
        }
        #region Context Menu Events
        private void launchEditPopupWindow(object sender, RoutedEventArgs e)
        {
            List<string> transactionHeaders = Transactions.getTransactionsColumns();
            List<string> transactionRow = this.controller.rowsToList(transactionDataGrid.SelectedItems);

            if (transactionRow.Count == transactionHeaders.Count)
            {
                Dictionary<string, string> transactionRowDictionary = transactionHeaders.ToDictionary(x => x, x => transactionRow[transactionHeaders.IndexOf(x)]);

                modifiedValues = new Dictionary<string, string>();
                EditWindow editTransactionWindow = new EditWindow(this, this.controller, transactionRowDictionary);
                editTransactionWindow.ShowDialog();
                if (modifiedValues.Count != 0)
                {
                    List<string> modifiedValueKeys = new List<string>(modifiedValues.Keys);
                    List<string> modifiedValuesValues = new List<string>(modifiedValues.Values);
                    this.controller.addTo("ModificationTable", modifiedValueKeys, modifiedValuesValues);
                }
            }
            else
            {
                MessageBox.Show("Select a transaction to edit first.", "Error");
            }
        }
        private void launchDeletePrompt(object sender, RoutedEventArgs e)
        {
            List<string> transactionHeaders = Transactions.getTransactionsColumns();
            List<string> transactionRow = this.controller.rowsToList(transactionDataGrid.SelectedItems);

            if (transactionRow.Count == transactionHeaders.Count)
            {
                this.controller.deleteTransaction(transactionRow[0]);
            }
            else
            {
                MessageBox.Show("Select a transaction to edit first.", "Error");
            }
        }
        private void launchEditHistoryWindow(object sender, RoutedEventArgs e)
        {
            List<string> transactionRow = this.controller.rowsToList(transactionDataGrid.SelectedItems);
            HistoryWindow editHistoryWindow = new HistoryWindow(this, this.controller, transactionRow[0]);
            editHistoryWindow.Show();
        }
        #endregion
        #region Button Click Events
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateAdded = (DateTime)dateAddedDatePicker.SelectedDate;
            List<string> transactionColumns = Transactions.getTransactionsColumns();
            transactionColumns = transactionColumns.GetRange(1, transactionColumns.Count - 2);

            List<string> transactionValues = new List<string>(new string[] {
                dateAdded.ToString("M/dd/yyyy"),
                amountTextBox.Text,
                transactionTypeTextBox.Text,
                categoryTextBox.Text,
                (descriptionTextBox.Text.Equals(defaultDescriptionText)) ? "" : descriptionTextBox.Text
            });

            this.controller.addTo(Transactions.table, transactionColumns, transactionValues);
            this.controller.refreshView();
        }
        private void originalBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            string origBalance = originalBalance.Content.ToString();
            Popup editOriginalBalance = new Popup(this, this.controller, origBalance);
            editOriginalBalance.ShowDialog();
            if (this.popupReturnButtonValue.Equals("Save") && this.controller.isNumeric(this.popupReturnValue))
            {
                this.controller.updateOriginalBalance(this.popupReturnValue);
                originalBalance.Content = "Original Balance: P" + this.controller.getOriginalBalance();
                this.controller.calculateCurrentBalance();
                currentBalanceValue.Content = "P " + this.controller.calculateCurrentBalance();
            }
        }
        #endregion

        private void dateAddedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            dateAddedDatePicker.SelectedDate = DateTime.Today;
            dateAddedDatePicker.IsEnabled = false;
        }
        private void dateAddedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            dateAddedDatePicker.IsEnabled = true;
        }
        private void amountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double result;
            if (!(double.TryParse(e.Text, out result) || (e.Text.Contains(".") && amountTextBox.Text.Count(x => x == '.') < 1)))
            {
                e.Handled = true;
            }
        }
        private void amountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (this.controller.isNumeric(amountTextBox.Text))
                {
                    double amount = Convert.ToDouble(amountTextBox.Text);
                    amountTextBox.Text = String.Format("{0:0.00}", amount);
                }
            }
            else if (e.Key.Equals(Key.OemMinus))
            {
                if (amountTextBox.Text.Contains('-'))
                {
                    amountTextBox.Text = amountTextBox.Text.Substring(1);
                }
                else
                {
                    amountTextBox.Text = "-" + amountTextBox.Text;
                }
                amountTextBox.CaretIndex = amountTextBox.Text.Length;
            }
        }
        private void roundOffDecimal(object sender, RoutedEventArgs e)
        {
            if (this.controller.isNumeric(amountTextBox.Text))
            {
                double amount = Convert.ToDouble(amountTextBox.Text);
                amountTextBox.Text = String.Format("{0:0.00}", amount);
                transactionTypeTextBox.Text = Transactions.getTransactionType(amount);
            }
            else
            {
                amountTextBox.Text = defaultAmountText;
                transactionTypeTextBox.Text = Transactions.revenue;

            }
        }

        private void descriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (descriptionTextBox.Foreground.Equals(Brushes.Gray))
            {
                descriptionTextBox.Foreground = Brushes.Black;
                descriptionTextBox.Text = "";
            }
        }

        private void descriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (descriptionTextBox.Text == "")
            {
                descriptionTextBox.Text = defaultDescriptionText;
                descriptionTextBox.Foreground = Brushes.Gray;
            }
        }
    }
}
