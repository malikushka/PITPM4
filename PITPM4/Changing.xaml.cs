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

namespace PITPM4
{
    public partial class Changing : Window
    {
        private Одежда productToChange;
        public Changing(Одежда selectedProduct)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            productToChange = selectedProduct;
            FillDataFields();
        }
        private void FillDataFields()
        {
            IdTextBox.Text = productToChange.ID.ToString();
            nameproductTextBox.Text = productToChange.Название;
            opisTextBox.Text = productToChange.Описание;
            priceTextBox.Text = productToChange.Цена.ToString();
            sizeTextBox.Text = productToChange.Размер;
        }
        private bool AreAllFieldsFilled()
        {
            if (string.IsNullOrEmpty(IdTextBox.Text) || string.IsNullOrEmpty(nameproductTextBox.Text) || string.IsNullOrEmpty(priceTextBox.Text)
                || string.IsNullOrEmpty(opisTextBox.Text) || string.IsNullOrEmpty(sizeTextBox.Text))
            {
                messageTextBlock.Text = "Все поля должны быть заполнены";
                return false;
            }
            return true;
        }

        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (!AreAllFieldsFilled())
            {
                return;
            }

            // Проверка ID на число
            int newId;
            if (!int.TryParse(IdTextBox.Text, out newId))
            {
                messageTextBlock.Text = "Введите корректный ID товара";
                return;
            }

            // Получаем данные о товаре из текстовых полей
            string newName = nameproductTextBox.Text;
            string newopis = opisTextBox.Text;
            string newprice = priceTextBox.Text;
            string newsize = sizeTextBox.Text;

            // Обновляем товар с новыми данными
            using (var context = new TRPOEntities())
            {
                var productToUpdate = context.Одежда.FirstOrDefault(p => p.ID == productToChange.ID);
                if (productToUpdate != null)
                {
                    productToUpdate.ID = newId;
                    productToUpdate.Название = newName;
                    productToUpdate.Описание = newopis;
                    productToUpdate.Цена = newprice;
                    productToUpdate.Размер = newsize;

                    context.SaveChanges();
                    MessageBox.Show("Товар успешно изменён!");
                    Close();
                }
            }
        }
        
        public void IdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        public void nameproductTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверить, что вводимый символ является буквой
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[а-яА-Я]+$"))
            {
                e.Handled = true; // Остановить ввод, если символ не является буквой
            }
        }
        public void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void opisTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
