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
using WPFProjekt.Models;
using WPFProjekt.Services.Interfaces;
using WPFProjekt.Services;
using System.ComponentModel;
using WPFProjekt.Models.Enums;

namespace WPFProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy EditNoteFormWindow.xaml
    /// </summary>
    public partial class EditNoteFormWindow : Window
    {
        private readonly INoteService _noteService = new NotesService();
        private readonly ICategoryService _categoryService = new CategoryService();

        public List<Category> Categories { get; set; }
       
        public Note Note { get; set; }
        public Category SelectedCategory { get; set; }

        public EditNoteFormWindow(Note note, Category category)
        {
            InitializeComponent();
            GetAllCategories();
           // GetCategoryById(note.Id);
            Note = note;


            TitleTextBlock.Text = Note.Title;
            //CatComboBox.SelectedIndex = CatComboBox.FindName
            PrioritySlider.Value = (double)Note.Priority;
            DescriptionTextBox.Text = Note.Content;


        }

        public async void GetAllCategories()
        {
            Categories = (List<Category>)await _categoryService.GetAllAsync();
            CatComboBox.ItemsSource = Categories;
            CatComboBox.SelectedIndex = 0;
        }

       

        private void EditNote(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBlock.Text;
            string content = DescriptionTextBox.Text;
            Category cat = (Category)CatComboBox.SelectedItem;
            Priority pr = (Priority)PrioritySlider.Value;


            /*
            Note note = new Note
            {
                Title = title,
                Content = content,
                Priority = pr,
                CategoryId = cat.Id,
            };
            */

            Note.Title = title;
            Note.Content = content;
            Note.Priority = pr;
            Note.CategoryId = cat.Id;
            Note.updateDateTime = DateTime.Now;

            eNote(Note);

            this.Close();
        }

        private async void eNote(Note note)
        {
            await _noteService.UpdateAsync(note);
        }
    }
}
