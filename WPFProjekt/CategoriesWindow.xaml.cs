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
using WPFProjekt.Services.Interfaces;
using WPFProjekt.Services;
using System.Collections.ObjectModel;
using WPFProjekt.Models;

namespace WPFProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {

        private readonly ICategoryService _categoryService = new CategoryService();
        private readonly INoteService _notesService = new NotesService();

        public ObservableCollection<Category> Categories;

        public CategoriesWindow()
        {
            InitializeComponent();
            GetAllCategories();

        }

        private async void GetAllCategories()
        {
            var tmp = await _categoryService.GetAllAsync();
            Categories = new ObservableCollection<Category>(tmp);

            CategoriesListBox.ItemsSource = Categories;
        }

        private async void AddCategory(Category category)
        {
           await _categoryService.AddAsync(category);
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            Category cat = new Category();
            cat.Name = TitleTextBox.Text;

            Categories.Add(cat);
            AddCategory(cat);
        }

        private void delCategory(object sender, RoutedEventArgs e)
        {
            Category cat = CategoriesListBox.SelectedItem as Category;
            if(cat is null)
            {
                MessageBox.Show("Zaznacz kategorie");
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure? All related notes will be deleted.", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    _categoryService.DeleteCategoryWithNotesById(cat);
                    Categories.Remove(cat);
                }
                
            }
        }

        private void CategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
