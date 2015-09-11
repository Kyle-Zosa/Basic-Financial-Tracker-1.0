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
using System.Windows.Shapes;

namespace Basic_Financial_Tracker
{
    public partial class EditWindow : Window
    {
        private MainView view;
        private Controller controller;

        public string title = "Edit Transaction ID ";
        public Dictionary<string, string> originalValues;

        public string defaultReasonForChange = "Type in the reason for change";
        public SolidColorBrush placeHolderColor = Brushes.Gray;
        public EditWindow(MainView view, Controller controller, Dictionary<string, string> originalValues)
        {
            this.originalValues = originalValues;
            this.view = view;
            this.controller = controller;

            InitializeComponent();

            Point mousePos = Mouse.GetPosition(this.view.MainWindow);
            editWindow.Top = mousePos.Y + this.view.MainWindow.Top - (editWindow.Height / 2);
            editWindow.Left = mousePos.X + this.view.MainWindow.Left - (editWindow.Width / 2);

            edit_ReasonForChangeTextBox.Text = defaultReasonForChange;
            edit_ReasonForChangeTextBox.Foreground = placeHolderColor;

            editWindow.Title = this.title += originalValues[Transactions.transactionID];
            editTransactionGroupBox.Header += originalValues[Transactions.transactionID];

            edit_TransactionIDTextBox.Text = originalValues[Transactions.transactionID];
            edit_TransactionTypeTextBox.Text = originalValues[Transactions.type];
            edit_CategoryTextBox.Text = originalValues[Transactions.category];
            edit_AmountTextBox.Text = originalValues[Transactions.amount];
            edit_DateAddedDatePicker.SelectedDate = this.controller.validateDate(originalValues[Transactions.dateCreated]);
            if (!originalValues[Transactions.dateModified].Equals(""))
                edit_DateModifiedDatePicker.SelectedDate = this.controller.validateDate(originalValues[Transactions.dateModified]);
            edit_DescriptionTextBox.Text = originalValues[Transactions.description];
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string newType = edit_TransactionTypeTextBox.Text;
            string newAmount = originalValues[Transactions.amount].Equals(edit_AmountTextBox.Text) ? "" : edit_AmountTextBox.Text;
            string newCategory = (originalValues[Transactions.category].Equals(edit_CategoryTextBox.Text)) ? "" : edit_CategoryTextBox.Text;
            string newDescription = (originalValues[Transactions.description].Equals(edit_DescriptionTextBox.Text)) ? "" : edit_DescriptionTextBox.Text;
            string reasonForChange = (edit_ReasonForChangeTextBox.Text.Equals(defaultReasonForChange)) ? "" : edit_ReasonForChangeTextBox.Text;
            DateTime previousDate = (DateTime)edit_DateModifiedDatePicker.SelectedDate;

            List<string> modificationsColumns = Modifications.getModificationsColumns();
            modificationsColumns = modificationsColumns.GetRange(1, modificationsColumns.Count - 1);

            if (!(newType == originalValues[Transactions.type] && newAmount == "" && newCategory == "" && newDescription == ""))
            {
                string updateAmount = (newAmount == "") ? originalValues[Transactions.amount] : newAmount;
                string updateCategory = (newCategory == "") ? originalValues[Transactions.category] : newCategory;
                string updateDescription = (newDescription == "") ? originalValues[Transactions.description] : newDescription;

                List<string> updateColumns = new List<string>(new string[] { Transactions.type, Transactions.amount, Transactions.category, Transactions.description, Transactions.dateModified });
                List<string> updateValues = new List<string>(new string[] { newType, updateAmount, updateCategory, updateDescription, DateTime.Today.ToString("M/dd/yyyy") });

                this.controller.updateEntry(Transactions.table, Transactions.transactionID, originalValues[Transactions.transactionID], updateColumns, updateValues);

                List<string> modificationValues = new List<string>(new string[] {
                    originalValues[Transactions.transactionID],
                    originalValues[Transactions.amount],
                    originalValues[Transactions.category],
                    originalValues[Transactions.description],
                    newAmount,
                    newCategory,
                    newDescription,
                    DateTime.Today.ToString("M/dd/yyyy"),
                    previousDate.ToString("M/dd/yyyy"),
                    reasonForChange
                });

                this.controller.addTo(Modifications.table, modificationsColumns, modificationValues);
                this.controller.refreshView();
            }
            else
                MessageBox.Show("No changes were made.");
            this.Close();
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void edit_ReasonForChangeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (edit_ReasonForChangeTextBox.Text.Equals(defaultReasonForChange))
            {
                edit_ReasonForChangeTextBox.Text = "";
                edit_ReasonForChangeTextBox.Foreground = Brushes.Black;
            }
        }
        private void edit_ReasonForChangeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (edit_ReasonForChangeTextBox.Text.Equals("") || edit_ReasonForChangeTextBox.Text.Equals(defaultReasonForChange))
            {
                edit_ReasonForChangeTextBox.Text = defaultReasonForChange;
                edit_ReasonForChangeTextBox.Foreground = placeHolderColor;
            }
        }
        private void edit_AmountTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (edit_AmountTextBox.Text.Equals(""))
            {
                edit_AmountTextBox.Text = originalValues[Transactions.amount];
            }
            else
            {
                double amount = Convert.ToDouble(edit_AmountTextBox.Text);
                edit_AmountTextBox.Text = String.Format("{0:0.00}", amount);
                edit_TransactionTypeTextBox.Text = Transactions.getTransactionType(amount);
            }
        }
        private void edit_AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double result;
            if (!(double.TryParse(e.Text, out result) || (e.Text.Contains(".") && edit_AmountTextBox.Text.Count(x => x == '.') < 1)))
            {
                e.Handled = true;
            }
        }
        private void edit_AmountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (!(edit_AmountTextBox.Text == ""))
                {
                    double amount = Convert.ToDouble(edit_AmountTextBox.Text);
                    edit_AmountTextBox.Text = String.Format("{0:0.00}", amount);
                }
            }
            else if (e.Key.Equals(Key.OemMinus))
            {
                if (edit_AmountTextBox.Text.Contains('-'))
                {
                    edit_AmountTextBox.Text = edit_AmountTextBox.Text.Substring(1);
                }
                else
                {
                    edit_AmountTextBox.Text = "-" + edit_AmountTextBox.Text;
                }
                edit_AmountTextBox.CaretIndex = edit_AmountTextBox.Text.Length;
            }
        }
    }
}
