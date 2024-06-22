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
    /// Логика взаимодействия для MechanicWindow.xaml
    /// </summary>
    public partial class MechanicWindow : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";

        private int masterId;

        public MechanicWindow(int masterId)
        {
            InitializeComponent();
            this.masterId = masterId;
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Requests WHERE masterID = @masterId", conn);
                da.SelectCommand.Parameters.AddWithValue("@masterId", masterId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                showshow.ItemsSource = dt.DefaultView;
            }
        }

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (showshow.SelectedItem != null)
            {
                DataRowView row = (DataRowView)showshow.SelectedItem;
                AddCommentWindow addCommentWindow = new AddCommentWindow((int)row["requestID"], masterId);
                addCommentWindow.Show();
            }
        }
    }
}