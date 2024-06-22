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
    /// Логика взаимодействия для AssignMasterWindow.xaml
    /// </summary>
    public partial class AssignMasterWindow : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";

        private int requestId;

        public AssignMasterWindow(int requestId)
        {
            InitializeComponent();
            this.requestId = requestId;
            LoadMasters();
        }

        private void LoadMasters()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT userID, fio FROM Users WHERE type = 'Мастер'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                MastersComboBox.ItemsSource = dt.DefaultView;
                MastersComboBox.DisplayMemberPath = "fio";
                MastersComboBox.SelectedValuePath = "userID";
            }
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Requests SET masterID = @masterID, requestStatus = 'В процессе ремонта' WHERE requestID = @requestId", conn);
                cmd.Parameters.AddWithValue("@masterID", MastersComboBox.SelectedValue);
                cmd.Parameters.AddWithValue("@requestId", requestId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Мастер назначен");
            this.Close();
        }
    }
}