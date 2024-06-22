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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void LoginWindowButton_Click(object sender, RoutedEventArgs e)
        {
            string fio = FIOTextBox.Text;
            string phone = PhoneTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fio) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO USERS (fio, phone, login, password) VALUES (@fio, @phone, @login, @password)", connection);
                    command.Parameters.AddWithValue("@fio", fio);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteNonQuery();

                }
                MessageBox.Show("Регистрация успешна");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message+"Ошибка, попробуйте еще раз");
            }
        }
    }
}
