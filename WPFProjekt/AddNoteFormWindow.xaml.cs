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
using WPFProjekt.Models.Enums;

namespace WPFProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy AddNoteFormWindow.xaml
    /// </summary>
    public partial class AddNoteFormWindow : Window
    {
        private readonly INoteService _noteService = new NotesService();
        private readonly ICategoryService _categoryService = new CategoryService();

        public List<Category> Categories { get; set; }

        public AddNoteFormWindow()
        {
            InitializeComponent();

            GetAllCategories();
                    
        }

        public async void GetAllCategories()
        {
            Categories = (List<Category>)await _categoryService.GetAllAsync(); 
            CatComboBox.ItemsSource = Categories;
            CatComboBox.SelectedIndex = 0;        
        }

        private async void AddNote(Note note)
        {
            await _noteService.AddAsync(note);
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBlock.Text;
            string content = DescriptionTextBox.Text;
            Category cat = (Category)CatComboBox.SelectedItem;
            Priority pr = (Priority)PrioritySlider.Value;

            if(title == null || title == "")
            {
                MessageBox.Show("Title cannot be empty!!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(content == null || content == "")
            {
                MessageBox.Show("Description cannot be empty!!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Note note = new Note
                {
                    Title = title,
                    Content = content,
                    Priority = pr,
                    CategoryId = cat.Id,
                    createDateTime = DateTime.Now,
                    updateDateTime = DateTime.Now,
                };

                AddNote(note);

                this.Close();
            }

            

        }
    }
}
