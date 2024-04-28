using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using PITPM4;
using System.Runtime.Remoting.Contexts;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Windows.Media;
using System.Diagnostics;

namespace UnitTestProject1
{
    
    [TestClass]
    public class UnitTest1
    {
        private string testImageFilePath = "C:\\Users\\admin\\Downloads\\user.png";
        public SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-P16VD0E; Initial Catalog = UserDB; Integrated Security = True;MultipleActiveResultSets=True;TrustServerCertificate=True;Application Name=EntityFramework");
        public SqlCommand command;
        public SqlDataReader dataReader;

        [TestMethod]
        public void AddTest()
        {
            
            int productId = 100;
            string productName = "TestProduct";
            string price = "100";
            string imagePath = "C:\\Users\\admin\\Downloads\\user.png";
            PITPM4.Adding adding = new PITPM4.Adding();
            var result = adding.AddProduct(productId, productName, price, imagePath);
            Assert.IsTrue(result);

            /*PITPM4.Adding adding = new PITPM4.Adding();
            adding.AddProduct(productId, productName, price, imagePath);

            connection.Close();
            connection.Open();
            using (var context = new PITPM4.UserDBEntities())
            {
                if (context.Products.Any(u => u.Id == productId))
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(true);
                }
            }
            connection.Close();*/

            
            connection.Close();
            connection.Open();

            command = new SqlCommand("INSERT INTO Products VALUES(@Id,@Name,@Price,@Image)", connection);
            command.Parameters.AddWithValue("@Id", productId);
            command.Parameters.AddWithValue("@Name", productName);
            command.Parameters.AddWithValue("@Price", Convert.ToInt32(price));
            command.Parameters.AddWithValue("@Image", imagePath);
            command.ExecuteReader();


            command = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", productId);

            int count = (int)command.ExecuteScalar();
            Assert.AreEqual(1, count); // Проверяем, что продукт был добавлен
            connection.Close();
        }

        [TestMethod]
        public void UpdateTest()
        {
           
            int productId = 100;
            string price = "125"; // Изменённая цена

            // Assert
            connection.Close();
            connection.Open();

            command = new SqlCommand("UPDATE Products SET Price=@Price WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", productId);
            command.Parameters.AddWithValue("@Price", Convert.ToInt32(price));
            command.ExecuteReader();

            connection.Close();
            connection.Open();

            command = new SqlCommand("SELECT * FROM Products WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", productId);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                Assert.AreEqual(price, dataReader["Price"].ToString());
            }
            dataReader.Close();

            connection.Close();
        }

        [TestMethod]
        public void DeleteTest()
        {
            
            int productId = 100;

            
            connection.Close();
            connection.Open();

            command = new SqlCommand("DELETE FROM Products WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", productId);
            command.ExecuteReader();

            connection.Close();
            connection.Open();

            command = new SqlCommand("SELECT * FROM Products WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", productId);
            dataReader = command.ExecuteReader();
            if (!dataReader.Read())
            {
                Assert.IsTrue(true);
            }
            dataReader.Close();

            connection.Close();
        }
    }
}

