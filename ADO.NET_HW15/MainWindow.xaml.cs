using ADO.NET_HW15.Models;
using ADO.NET_HW2;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO.NET_HW15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void showAllBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    var list = db.List.FromSqlRaw("Select * from List").ToList();
                    dataGridView.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void showNamesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    var names = db.List.FromSqlRaw("Select Name from List")
                        .Select(n => new 
                        {
                            Назва = n.Name
                        }).ToList();

                    dataGridView.ItemsSource = names;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void showColorsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    var colors = db.List.FromSqlRaw("Select distinct Color from List")
                        .Select(c => new
                        {
                            Колір = c.Color
                        }).ToList();

                    dataGridView.ItemsSource = colors;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void minCaloriesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@calories",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    db.Database.ExecuteSqlRaw("MinOfCalories @calories out", param);

                    int calories = (int)param.Value;
                    MessageBox.Show($"Найменша калорійність: {calories}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void maxCaloriesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@calories",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    db.Database.ExecuteSqlRaw("MaxOfCalories @calories out", param);

                    int calories = (int)param.Value;
                    MessageBox.Show($"Найбільша калорійність: {calories}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void avgCaloriesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@calories",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    db.Database.ExecuteSqlRaw("AvgOfCalories @calories out", param);

                    int calories = (int)param.Value;
                    MessageBox.Show($"Середня калорійність: {calories}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countVegetablesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@vegetables",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    db.Database.ExecuteSqlRaw("CountVegetables @vegetables out", param);

                    int vegetables = (int)param.Value;
                    MessageBox.Show($"Загальна кількість овочів: {vegetables}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countFruitsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@fruits",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    db.Database.ExecuteSqlRaw("CountFruits @fruits out", param);

                    int fruits = (int)param.Value;
                    MessageBox.Show($"Загальна кількість фруктів: {fruits}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countByChosenColorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? colorInput = InputPrompt.ShowDialog("Уведіть колір:");
                if (!string.IsNullOrWhiteSpace(colorInput))
                {
                    using (FruitsAndVegetablesDbContext db = new())
                    {
                        SqlParameter inputParam = new("@color", colorInput);
                        SqlParameter outputParam = new()
                        {
                            ParameterName = "@quantity",
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.Output
                        };

                        db.Database.ExecuteSqlRaw("CountByColor @color, @quantity out", inputParam, outputParam);

                        int quantity = (int)outputParam.Value;
                        MessageBox.Show($"Кількість овочів і фруктів кольору \"{colorInput}\": {quantity}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countEachColorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    var eachColor = db.List.FromSqlRaw("Exec CountEachColor").ToList();
                    dataGridView.ItemsSource = eachColor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void caloriesBelowValueBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? caloriesInput = InputPrompt.ShowDialog("Уведіть кількість калорій:");
                if (!string.IsNullOrWhiteSpace(caloriesInput) && int.TryParse(caloriesInput, out int calories))
                {
                    using (FruitsAndVegetablesDbContext db = new())
                    {
                        SqlParameter inputParam = new("@calories", calories);

                        var belowValue = db.List.FromSqlRaw("Exec CaloriesBelowValue @calories", inputParam).ToList();
                        dataGridView.ItemsSource = belowValue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void caloriesAboveValueBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? caloriesInput = InputPrompt.ShowDialog("Уведіть кількість калорій:");
                if (!string.IsNullOrWhiteSpace(caloriesInput) && int.TryParse(caloriesInput, out int calories))
                {
                    using (FruitsAndVegetablesDbContext db = new())
                    {
                        SqlParameter inputParam = new("@calories", calories);

                        var aboveValue = db.List.FromSqlRaw("Exec CaloriesAboveValue @calories", inputParam).ToList();
                        dataGridView.ItemsSource = aboveValue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void caloriesInRangeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? minCaloriesInput = InputPrompt.ShowDialog("Уведіть найменшу кількість калорій:");
                string? maxCaloriesInput = InputPrompt.ShowDialog("Уведіть найбільшу кількість калорій:");
                if ((!string.IsNullOrWhiteSpace(minCaloriesInput) && int.TryParse(minCaloriesInput, out int minCalories))
                    && (!string.IsNullOrWhiteSpace(maxCaloriesInput) && int.TryParse(maxCaloriesInput, out int maxCalories)))
                {
                    if (minCalories > maxCalories || maxCalories < minCalories)
                    {
                        MessageBox.Show("Найменше значення більше за найбільше. Спробуйте заповнити поля ще раз", "Ой", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    using (FruitsAndVegetablesDbContext db = new())
                    {
                        SqlParameter inputParam1 = new("@minCalories", minCalories);
                        SqlParameter inputParam2 = new("@maxCalories", maxCalories);

                        var range = db.List.FromSqlRaw("Exec CaloriesInRange @minCalories, @maxCalories", inputParam1, inputParam2).ToList();
                        dataGridView.ItemsSource = range;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void yellowOrRedColorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FruitsAndVegetablesDbContext db = new())
                {
                    var redOrYellow = db.List.FromSqlRaw("Select * from List where Color = N'Жовтий' or Color = N'Червоний'").ToList();
                    dataGridView.ItemsSource = redOrYellow;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void openDataHandlingBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataHandling dataHandling = new DataHandling();
                dataHandling.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}