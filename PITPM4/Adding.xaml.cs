using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.IO;
using System.Security.Policy;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace PITPM4
{
    public partial class Adding : Window
    {
        string connectionString = "Data Source=DESKTOP-P16VD0E;Initial Catalog=TRPO;Integrated Security=True;MultipleActiveResultSets=True";
        public Adding()
        {
            InitializeComponent();
            this.Left= 0;
            this.Top= 0;
        }
        public void AddProduct(int clothingId, string name, string description, decimal price, string size)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Проверка на существование Id в базе данных
                string checkQuery = "SELECT COUNT(*) FROM Одежда WHERE ID = @Id";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Id", clothingId);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Товар с таким Id уже существует");
                }
                else
                {
                    string query = "INSERT INTO Одежда (ID, Название, Описание, Цена, Размер) " +
                                   "VALUES (@Id, @Name, @Description, @Price, @Size)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", clothingId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Size", size);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Одежда успешно добавлена");
                        Close(); // Закрыть окно добавления товара
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при добавлении одежды: " + ex.Message);
                    }
                }
            }
        }
        public void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IdTextBox.Text, out int clothingId) && decimal.TryParse(priceTextBox.Text, out decimal price))
            {
                string name = nameproductTextBox.Text;
                string description = описTextBox.Text;
                string size = sizeTextBox.Text;

                AddProduct(clothingId, name, description, price, size);

                // Очистить поля после добавления
                IdTextBox.Text = "";
                nameproductTextBox.Text = "";
                описTextBox.Text = "";
                priceTextBox.Text = "";
                sizeTextBox.Text = "";
            }
            else
            {
                messageTextBlock.Text = "Пожалуйста, введите корректные данные для добавления одежды.";
            }
        }
        private void IdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверить, что вводимый символ является цифрой
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Остановить ввод, если символ не является цифрой
            }

            // Проверить, что общее количество введенных символов не превышает нужное количество
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 3 && e.Text.Length > 0)
            {
                e.Handled = true; // Остановить ввод, если количество введенных символов превышает 3
            }
        }

        private void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверить, что вводимый символ является цифрой
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Остановить ввод, если символ не является цифрой
            }

            // Проверить, что общее количество введенных символов не превышает нужное количество
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 4 && e.Text.Length > 0)
            {
                e.Handled = true; // Остановить ввод, если количество введенных символов превышает 3
            }
        }
        private void nameproductTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверить, что вводимый символ является буквой
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[а-яА-Я]+$"))
            {
                e.Handled = true; // Остановить ввод, если символ не является буквой
            }
        }
        private bool AreAllFieldsFilled()
        {
            if (string.IsNullOrEmpty(IdTextBox.Text) || string.IsNullOrEmpty(nameproductTextBox.Text) || string.IsNullOrEmpty(priceTextBox.Text) 
                || string.IsNullOrEmpty(описTextBox.Text) || string.IsNullOrEmpty(sizeTextBox.Text))
            {
                messageTextBlock.Text = "Все поля должны быть заполнены";
                return false;
            }
            return true;
        }

        private void описTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверить, что вводимый символ является буквой
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[а-яА-Я]+$"))
            {
                e.Handled = true; // Остановить ввод, если символ не является буквой
            }
        }

        private void priceTextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            // Проверить, что вводимый символ является цифрой
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Остановить ввод, если символ не является цифрой
            }

            // Проверить, что общее количество введенных символов не превышает нужное количество
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 5 && e.Text.Length > 0)
            {
                e.Handled = true; // Остановить ввод, если количество введенных символов превышает 3
            }
        }

        private void sizeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверить, что вводимый символ является буквой
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[A-Z]+$"))
            {
                e.Handled = true; // Остановить ввод, если символ не является буквой
            }
        }
    }
}
