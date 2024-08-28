using ADO.NET_HW15.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ADO.NET_HW15
{
    /// <summary>
    /// Interaction logic for DataHandling.xaml
    /// </summary>
    public partial class DataHandling : Window
    {
        public DataHandling()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //DataTable ids = await dbProvider.GetIdsAsync();
                using (FruitsAndVegetablesDbContext db = new())
                {
                    var ids = db.List.FromSqlRaw("Select Id from List")
                        .Select(id => id.ToString())
                        .ToList();
                    idComboBox.ItemsSource = ids;
                    idComboBox.DisplayMemberPath = "Id";
                    idComboBox.SelectedValuePath = "Id";
                    idComboBox.SelectedIndex = 0;

                    //DataTable types = await dbProvider.GetTypesAsync();
                    var types = db.List.FromSqlRaw("Select distinct Type from List")
                        .Select(t => new
                        {
                            t.Type
                        }).ToList();
                    typeComboBox.ItemsSource = types;
                    typeComboBox.DisplayMemberPath = "Type";
                    typeComboBox.SelectedValuePath = "Type";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void idComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int id = (int)idComboBox.SelectedValue;

                using (FruitsAndVegetablesDbContext db = new())
                {
                    SqlParameter param = new("@id", id);
                    
                    var valuesById = db.List.FromSqlRaw("Select * from List where Id = @id", id).ToList();
                    if (valuesById.Count > 0)
                    {
                        var item = valuesById.FirstOrDefault();
                        nameTxtBox.Text = item.Name;
                        typeComboBox.SelectedValue = item.Type;
                        colorTxtBox.Text = item.Color;
                        caloricContentTxtBox.Text = item.CaloricContent.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    string name = nameTxtBox.Text;
                    string type = typeComboBox.Text;
                    string color = colorTxtBox.Text;
                    string caloricContent = caloricContentTxtBox.Text;

                    SqlParameter[] parameters = {
                        new SqlParameter("name", name),
                        new SqlParameter("type", type),
                        new SqlParameter("color", color),
                        new SqlParameter("calories", caloricContent)
                    };
                    
                    db.Database.ExecuteSqlRaw("AddProduct @name, @type, @color, @calories", parameters);

                    MessageBox.Show("Дані додано успішно", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    int id = (int)idComboBox.SelectedValue;
                    string name = nameTxtBox.Text;
                    string type = typeComboBox.Text;
                    string color = colorTxtBox.Text;
                    string caloricContent = caloricContentTxtBox.Text;

                    SqlParameter[] parameters = {
                        new SqlParameter("id", id),
                        new SqlParameter("name", name),
                        new SqlParameter("type", type),
                        new SqlParameter("color", color),
                        new SqlParameter("calories", caloricContent)
                    };

                    int rowsAffected = db.Database.ExecuteSqlRaw("UpdateProduct @id, @name, @type, @color, @calories", parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Дані оновлено успішно", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Дані не збігаються за порядковим номером (Id)", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = (int)idComboBox.SelectedValue;

                using (FruitsAndVegetablesDbContext db = new()) 
                {
                    SqlParameter param = new("@id", id);

                    int rowsAffected = db.Database.ExecuteSqlRaw("DeleteProduct @id", param);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Дані видалено успішно", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Дані не збігаються за порядковим номером (Id)", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}