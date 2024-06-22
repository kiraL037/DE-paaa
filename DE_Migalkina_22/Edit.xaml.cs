using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";
        private int requestID;

        public Edit(int resuestID)
        {
            InitializeComponent();
            this.requestID = resuestID;
            LoadRequest();
        }

        private void LoadRequest()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Requests WHERE requestID=@requestID", connection);
                command.Parameters.AddWithValue("@requestID", requestID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    carTypeTextBox.Text = reader["carType"].ToString();
                    carModelTextBox.Text = reader["carModel"].ToString();
                    problemDescriptionTextBox.Text = reader["problemDescription"].ToString();
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Requests SET carType=@carType, carModel=@carModel, problemDescription=@problemDescription WHERE requestID=@requestID", connection);
                    command.Parameters.AddWithValue("@carType", carTypeTextBox.Text);
                    command.Parameters.AddWithValue("@carModel", carModelTextBox.Text);
                    command.Parameters.AddWithValue("@problemDescription", problemDescriptionTextBox.Text);
                    command.Parameters.AddWithValue("@requestID", requestID);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка, попробуйте еще раз");
            }
        }
    }
}
