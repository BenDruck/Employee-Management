using System.Windows;
using System.Windows.Controls;


static class Constants
{
    public const string Path = "Employees.csv";
}


namespace EmployeeManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Activated(object sender, System.EventArgs e)
        {
            RefreshList();
        }


        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            var inputWindow = new InputWindow();

            inputWindow.ShowDialog();
        }


        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            int databasePosition = Employee.GetDatabasePosition( GetSelectedCell(0) );

            var inputWindow = new InputWindow( databasePosition, GetSelectedCell(0) );

            inputWindow.inputFirst.Text = GetSelectedCell(1);
            inputWindow.inputLast.Text = GetSelectedCell(2);
            inputWindow.inputDOB.Text = GetSelectedCell(4);
            inputWindow.inputEmail.Text = GetSelectedCell(5);
            inputWindow.inputPhone.Text = GetSelectedCell(6);
            inputWindow.inputAccess.Text = GetSelectedCell(7);

            inputWindow.ShowDialog();
        }


        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            string id = GetSelectedCell(0);

            int position = Employee.GetDatabasePosition(id);

            Employee.DeleteEmployee(position);

            RefreshList();
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterDataGrid();
        }


        private void FilterDrop_SelectionChanged(object sender, System.EventArgs e)
        {
            FilterDataGrid();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                ButtonsEnabled(true);

                displayID.Text = GetSelectedCell(0);
                displayFirst.Text = GetSelectedCell(1);
                displayLast.Text = GetSelectedCell(2);
                displayLogin.Text = GetSelectedCell(3);
                displayDOB.Text = GetSelectedCell(4);
                displayEmail.Text = GetSelectedCell(5);
                displayPhone.Text = GetSelectedCell(6);
                displayAccess.Text = GetSelectedCell(7);
            }
            else
            {
                ButtonsEnabled(false);
            }
        }


        public void ButtonsEnabled(bool enabled)
        {
            button_Edit.IsEnabled = enabled;

            button_Delete.IsEnabled = enabled;
        }


        public void RefreshList()
        {
            filterAccess.SelectedIndex = 0;

            searchBox.Clear();

            dataGrid.Items.Clear();

            var employees = Database.Read();

            foreach (var employee in employees)
            {
                dataGrid.Items.Add(employee);
            }

            foreach (var column in dataGrid.Columns)
            {
                column.SortDirection = null;

                column.Width = DataGridLength.SizeToCells;
            }
        }


        private void FilterDataGrid()
        {
            bool access = !filterAccess.SelectedIndex.Equals(0);

            bool search = searchBox.Text.Equals("") ? false : true;

            if (!search && !access)
            {
                RefreshList();
            }
            else
            {
                dataGrid.Items.Clear();

                var employees = Database.Read();

                if (access)
                {
                    for (int i= employees.Count-1; i>=0; i--)
                    {
                        string[] data = Employee.GetEmployeeData(employees[i]);

                        if ( !((ComboBoxItem)filterAccess.SelectedValue).Content.ToString().Equals(data[7]) )
                        {
                            employees.RemoveAt(i);
                        }
                    }
                }

                if (search)
                {
                    for (int i=employees.Count-1; i>=0; i--)
                    {
                        string[] data = Employee.GetEmployeeData(employees[i]);

                        for(int j = 0; j < data.Length; j++)
                        {
                            if ( data[j].ToUpper().Contains(searchBox.Text.ToUpper()) )    
                            {
                                break;
                            }

                            if (j == data.Length - 1)
                            {
                                employees.RemoveAt(i);
                            }
                        }
                    }
                }

                foreach (var employee in employees)
                {
                    dataGrid.Items.Add(employee);
                }
            }
        }


        private int GetSelectedPosition()
        {
            return dataGrid.Items.IndexOf(dataGrid.SelectedItem);
        }


        private string GetSelectedCell(int column)
        {
            var cellInfo = dataGrid.SelectedCells[column];

            string content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

            return content;
        }

    }
}
