using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public List<Clothing> ClothingList { get; set; }
        public Admin()
        {
            InitializeComponent();
            ClothingList = GetClothingData(); // Получение данных из базы и заполнение списка
            DataContext = this;
        }
        private List<Clothing> GetClothingData()
        {
            List<Clothing> clothingList = new List<Clothing>();

            using (var context = new TRPOEntities()) // TRPOEntities - ваш контекст данных Entity Framework
            {
                var clothingEntities = context.Одежда.ToList(); // Получаем все записи из таблицы Одежда

                foreach (var clothingEntity in clothingEntities)
                {
                    Clothing clothing = new Clothing
                    {
                        Id = clothingEntity.ID,
                        Name = clothingEntity.Название,
                        Description = clothingEntity.Описание,
                        Price = Convert.ToDecimal(clothingEntity.Цена), // Явное преобразование строки в decimal
                        Size = clothingEntity.Размер
                    };

                    clothingList.Add(clothing);
                }
            }

            return clothingList;
        }
        private void Adding_Click(object sender, RoutedEventArgs e)
        {
            Adding addingWindow = new Adding();
            addingWindow.Show();
        }
        

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            int productId;
            if(!int.TryParse(txtProductId.Text, out productId))
            {
                MessageBox.Show("Введите корректный ID товара");
                return;
            }
            using (var context = new TRPOEntities())
            {
                var productToChange = context.Одежда.FirstOrDefault(p => p.ID == productId);
                if(productToChange != null)
                {
                    Changing changeWindow = new Changing(productToChange);
                    changeWindow.Show();
                }
                else
                {
                    MessageBox.Show("Продукт с указанным ID не найден");
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var context = new TRPOEntities())
            {
                var productToDelete = context.Одежда.FirstOrDefault(p => p.ID == productId);
                if (productToDelete != null)
                {
                    context.Одежда.Remove(productToDelete);
                    context.SaveChanges();
                }
            }
        }

        public void Delite(int productId)
        {
            using (var context = new TRPOEntities())
            {
                // Поиск товара по Id
                var productToDelete = context.Одежда.FirstOrDefault(p => p.ID == productId);
                if (productToDelete != null)
                {
                    // Удаление товара из базы данных
                    context.Одежда.Remove(productToDelete);
                    context.SaveChanges();
                    MessageBox.Show("Товар успешно удален");
                }
                else
                {
                    MessageBox.Show("Товар с указанным ID не найден");
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtProductId.Text, out int productId))
            {
                MessageBox.Show("Введите корректный ID товара");
                return;
            }
            Delite(productId);
        }

        private void txtProductId_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtProductId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Obn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Admin adminWindow = new Admin();
            adminWindow.Show();
        }
    }
}
