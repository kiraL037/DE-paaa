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
            this.requestID = requestID;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Requests (startDate, carType, carModel, problemDescription, 'Новая заявка', requestID)" +
                        " VALUES (@startDate, @carType, @carModel, @problemDescription, @password, @requestID)", connection);
                    command.Parameters.AddWithValue("@startDate", DateTime.Now);
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
