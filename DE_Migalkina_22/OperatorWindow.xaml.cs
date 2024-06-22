using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace DE_Migalkina_22
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";

        public OperatorWindow()
        {
            InitializeComponent(); LoadRequests();
        }

        private void LoadRequests()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Requests", connection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                showshow.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
