using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Popup : Window
    {
        MainView view;
        Controller controller;

        public string defaultTextBoxText = "Insert New Balance";
        public Popup(MainView view, Controller controller, string originalBalance)
        {
            InitializeComponent();

            this.view = view;
            this.controller = controller;

            popupLabel.Content = originalBalance;
        }

        private void aPopupButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.controller.isNumeric(popupTextBox.Text))
            {
                double amount = Convert.ToDouble(popupTextBox.Text);
                popupTextBox.Text = String.Format("{0:0.00}", amount);

                this.view.popupReturnButtonValue = "Save";
                this.view.popupReturnValue = popupTextBox.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please input valid number.", "Invalid Input");
            }
        }
        private void bPopupButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void popupTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double result;
            if (!(double.TryParse(e.Text, out result) || (e.Text.Contains(".") && popupTextBox.Text.Count(x => x == '.') < 1)))
            {
                e.Handled = true;
            }
        }
        private void popupTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (this.controller.isNumeric(popupTextBox.Text))
                {
                    double amount = Convert.ToDouble(popupTextBox.Text);
                    popupTextBox.Text = String.Format("{0:0.00}", amount);
                }
            }
            else if (e.Key.Equals(Key.OemMinus))
            {
                if (popupTextBox.Text.Contains('-'))
                {
                    popupTextBox.Text = popupTextBox.Text.Substring(1);
                }
                else
                {
                    popupTextBox.Text = "-" + popupTextBox.Text;
                }
                popupTextBox.CaretIndex = popupTextBox.Text.Length;
            }
        }
        private void popupTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.controller.isNumeric(popupTextBox.Text))
            {
                double amount = Convert.ToDouble(popupTextBox.Text);
                popupTextBox.Text = String.Format("{0:0.00}", amount);
            }
            else
            {
                popupTextBox.Text = defaultTextBoxText;
                popupTextBox.Foreground = Brushes.Gray;
            }
        }
        private void popupTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (popupTextBox.Foreground.Equals(Brushes.Gray))
            {
                popupTextBox.Foreground = Brushes.Black;
                popupTextBox.Text = "";
            }
        }
    }
}
