using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";
        private int clientID;

        public ClientWindow(int clientID)
        {
            InitializeComponent();
            this.clientID = clientID;
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Requests WHERE clientID=@clientID", connection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@clientID", clientID);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                showshow.ItemsSource = dataTable.DefaultView;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (showshow.SelectedItems != null)
                {
                    DataRowView dataRowView = (DataRowView)showshow.SelectedItems;
                    Edit edit = new Edit((int)dataRowView["@requestID"]);
                    edit.Show();
                }
                else
                {
                    MessageBox.Show("Выберите заявку");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Нет заявок"+ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Add add = new Add(clientID);
            add.Show();
        }
    }
}
