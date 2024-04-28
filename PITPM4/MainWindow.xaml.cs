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

namespace PITPM4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        private void AuthorizationWindow_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            string connectionString = "Data Source=DESKTOP-P16VD0E;Initial Catalog=TRPO;Integrated Security=True;MultipleActiveResultSets=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Close();
                connection.Open();

                // Проверяем в таблице Менеджер
                SqlCommand commandMenedjer = new SqlCommand("SELECT COUNT(*) FROM Менеджер WHERE Почта = @username AND Пароль = @password", connection);
                commandMenedjer.Parameters.AddWithValue("@username", username);
                commandMenedjer.Parameters.AddWithValue("@password", password);

                int countMenedjer = (int)commandMenedjer.ExecuteScalar();

                // Проверяем в таблице Продавец_Консультант
                SqlCommand commandKonsultant = new SqlCommand("SELECT COUNT(*) FROM [Продавец/Консультант] WHERE Почта = @username AND Пароль = @password", connection);
                commandKonsultant.Parameters.AddWithValue("@username", username);
                commandKonsultant.Parameters.AddWithValue("@password", password);

                int countKonsultant = (int)commandKonsultant.ExecuteScalar();

                if (countMenedjer > 0)
                {
                    
                    Menedjer menedjer = new Menedjer { Почта = username, Пароль = password };
                    Admin AdminWindow = new Admin();
                    AdminWindow.Show();
                  
                }
                else if (countKonsultant > 0)
                {
                    
                    Konsultant konsultant = new Konsultant { Почта = username, Пароль = password };
                    Klient KliWindow = new Klient();
                    KliWindow.Show();
                    
                }
                else
                {
                    messageTextBlock.Text = "Логин или пароль неверные!";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

