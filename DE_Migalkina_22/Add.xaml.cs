using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";
        private int clientID;

        public Add(int clientID)
        {
            InitializeComponent();
            this.clientID = clientID;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Requests (startDate, carType, carModel, problemDescription, 'Новая заявка', clientID)" +
                        " VALUES (@startDate, @carType, @carModel, @problemDescription, @password, @clientID)", connection);
                    command.Parameters.AddWithValue("@startDate", DateTime.Now); 
                    command.Parameters.AddWithValue("@carType", carTypeTextBox.Text);
                    command.Parameters.AddWithValue("@carModel", carModelTextBox.Text);
                    command.Parameters.AddWithValue("@problemDescription", problemDescriptionTextBox.Text);
                    command.Parameters.AddWithValue("@clientID", clientID);
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
