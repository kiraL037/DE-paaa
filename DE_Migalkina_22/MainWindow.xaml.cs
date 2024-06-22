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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DE_Migalkina_22
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=kjhdg;Integrated Security=True;";
        private int clientID;

        //private string con = "Server=LAPTOP-BP9G4DP1\\SQLEXPRESS;Database=User10;User Id=User10;Password=password;";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;

            if (string.IsNullOrEmpty(login)||string.IsNullOrEmpty(password)) 
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE login=@login and password=@password", connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int userID = (int)reader["userID"];
                        int roleID = (int)reader["roleID"];

                        OpenWindowByRole(userID, roleID);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Данные введены неверно");
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenWindowByRole(int userID, int roleID)
        {
            switch (roleID)
            {
                case 1: //Клиент Мастер Оператор Менеджер
                    ClientWindow clientWindow = new ClientWindow(clientID);
                    clientWindow.Show();
                    break;
                case 2:
                    MechanicWindow mechanicWindow = new MechanicWindow();
                    mechanicWindow.Show();
                    break;
                case 3:
                    OperatorWindow operatorWindow= new OperatorWindow();
                    operatorWindow.Show();
                    break;
                case 4:
                    ManagerWindow managerWindow = new ManagerWindow();
                    managerWindow.Show();
                    break;
                default:
                    MessageBox.Show("Проверьте введеные данные, роль не опознана");
                    break;
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow= new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
