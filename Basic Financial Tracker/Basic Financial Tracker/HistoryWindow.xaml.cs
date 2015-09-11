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
    public partial class HistoryWindow : Window
    {
        Controller controller;
        MainView view;
        public HistoryWindow(MainView view, Controller controller, string transactionID)
        {
            InitializeComponent();

            this.controller = controller;
            this.view = view;

            Point mousePos = Mouse.GetPosition(this.view.MainWindow);
            editHistory.Top = mousePos.Y + this.view.MainWindow.Top - (editHistory.Height / 2);
            editHistory.Left = mousePos.X + this.view.MainWindow.Left - (editHistory.Width / 2);

            editHistoryLabel.Content += transactionID + " History";
            editHistoryDataGrid.DataContext = this.controller.populateEditHistoryDataGrid(this, transactionID);
        }
    }
}
