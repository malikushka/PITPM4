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
    public partial class Klient : Window
    {
        public List<Clothing> ClothingList { get; set; }
        public Klient()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
