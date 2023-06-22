using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPFProjekt.Models;
using WPFProjekt.Services.Interfaces;
using WPFProjekt.Services;

namespace WPFProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy PdfExportFormWindow.xaml
    /// </summary>
    public partial class PdfExportFormWindow : Window
    {

        private readonly INoteService _noteService = new NotesService();
        private readonly ICategoryService _categoryService = new CategoryService();

        public ObservableCollection<Category> Categories;

        public PdfExportFormWindow()
        {
            InitializeComponent();

            GetAllCategories();
        }

        private async void GetAllCategories()
        {
            var tmp = await _categoryService.GetAllAsync();
            Categories = new ObservableCollection<Category>(tmp);

            CategoriesComboBox.ItemsSource = Categories;
        }

        private void AllCB_Checked(object sender, RoutedEventArgs e)
        {
            CategoriesComboBox.IsEnabled = false;
        }

        private void AllCB_Unchecked(object sender, RoutedEventArgs e)
        {
            CategoriesComboBox.IsEnabled = true;
        }
    }
}
