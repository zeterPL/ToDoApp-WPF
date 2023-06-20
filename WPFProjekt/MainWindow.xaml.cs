using Microsoft.EntityFrameworkCore.Metadata;
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

        public ObservableCollection<Note> NotesList { get; set; } = new ObservableCollection<Note>();

        public MainWindow()
        {
            InitializeComponent();
            
            GetAllNotes();          
        }



        public async void GetAllNotes()
        {
            var tmp = await _noteService.GetAllAsync();
            this.NotesList = new ObservableCollection<Note>(tmp);
            NotesListBox.ItemsSource = NotesList;
        }

        /*
        //Test dodawania
        private async void AddNote(object sender, RoutedEventArgs e)
        {
            Category cat = new Category
            {
                Name = "Kategoria 1",
            };

            var newNote = new Note
            {
                Title = "Note",
                Content = "To jest testowa notatka",
                Priority = Priority.Low,
                Category = cat,
            };

            await _noteService.AddAsync(newNote);
            this.NotesList.Add(newNote);
        }
        */
        private async void AddNote(object sender, RoutedEventArgs e)
        {
            AddNoteFormWindow w = new AddNoteFormWindow();
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;       
            w.ShowDialog();

            GetAllNotes();
        }

        private void delNote(object sender, RoutedEventArgs e)
        {
            this.NotesList.RemoveAt(0);
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void SortByPriority(object sender, RoutedEventArgs e)
        {
            var tmp = NotesList.OrderBy(t => t.Priority).ToList();
            NotesList = new ObservableCollection<Note>(tmp);
            NotesListBox.ItemsSource = NotesList;
        }

        public void SortByTitle(object sender, RoutedEventArgs e)
        {
            var tmp = NotesList.OrderBy(t => t.Title).ToList();
            NotesList = new ObservableCollection<Note>(tmp);
            NotesListBox.ItemsSource = NotesList;
        }

        public void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            if(SearchTextBox.Text != "")
            {
                var tmp = NotesList.Where(t => t.Title.Contains(SearchTextBox.Text)).ToList();
                NotesList = new ObservableCollection<Note>(tmp);
                NotesListBox.ItemsSource = NotesList;
            }
            else if(SearchTextBox.Text == "")
            {
                GetAllNotes();
            }
            
        }

        public void ResetFilters(object sender, RoutedEventArgs e)
        {
            GetAllNotes();
        }



    }

    
}
