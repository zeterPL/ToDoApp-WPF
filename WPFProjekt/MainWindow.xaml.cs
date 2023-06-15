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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFProjekt.Models;
using WPFProjekt.Models.Enums;
using WPFProjekt.Services;
using WPFProjekt.Services.Interfaces;

namespace WPFProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly INoteService _noteService = new NotesService();
        private readonly ICategoryService _categoryService = new CategoryService();

        public List<Note> NotesList { get; set; } = new List<Note>();

        public MainWindow()
        {
            InitializeComponent();

            GetAllNotes();          
        }

        public async void GetAllNotes()
        {
            this.NotesList = (List<Note>)await _noteService.GetAllAsync();
            NotesListBox.ItemsSource = NotesList;
        }


        //Test dodawania
        private async void AddNote(object sender, RoutedEventArgs e)
        {
            Category cat = new Category
            {
                Name = "Kategoria 1",
            };

            var newNote = new Note
            {
                Title = "test",
                Content = "To jest testowa notatka",
                Priority = Priority.Normal,
                Category = cat,
            };

            await _noteService.AddAsync(newNote);
        }
    }
}
