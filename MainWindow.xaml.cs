using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;

namespace CookbookRecipes
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;

        public MainWindow()
        {
            InitializeComponent();

            recipes = new List<Recipe>
            {
                new Recipe
                {
                    Title = "Суп",
                    ImagePath = "./img/Soup.jpg",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Product = "Картошка", Quantity = "300 г" },
                        new Ingredient { Product = "Морковь", Quantity = "150 г" },
                        new Ingredient { Product = "Лук", Quantity = "1 шт" },
                        new Ingredient { Product = "Мясо", Quantity = "200 г" },
                        new Ingredient { Product = "Соль", Quantity = "по вкусу" },
                        new Ingredient { Product = "Перец", Quantity = "по вкусу" }
                    },
                    Description = "Варите все ингредиенты вместе. Приятного аппетита!"
                },
                // Добавьте другие рецепты по аналогии
            };

            recipeListBox.ItemsSource = recipes;

            recipeListBox.SelectionChanged += RecipeListBox_SelectionChanged;

            // Вызываем обработчик для отображения информации о первом рецепте при запуске приложения
            RecipeListBox_SelectionChanged(null, null);
        }

        private void RecipeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Recipe selectedRecipe = recipeListBox.SelectedItem as Recipe;

            if (selectedRecipe != null)
            {
                recipeTitle.Text = selectedRecipe.Title;
                LoadImage(selectedRecipe.ImagePath, recipeImage);
                ingredientsDataGrid.ItemsSource = selectedRecipe.Ingredients;
                recipeDescription.Document.Blocks.Clear();
                recipeDescription.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(selectedRecipe.Description)));
            }
        }

        private void LoadImage(string path, Image imageControl)
        {
            try
            {
                imageControl.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, path)));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }
        }
    }
}

    public class Recipe
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Description { get; set; }
    }

    public class Ingredient
    {
        public string Product { get; set; }
        public string Quantity { get; set; }
    }
