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

        public ObservableCollection<Category> Categories;
        public ObservableCollection<Note> NotesList { get; set; } = new ObservableCollection<Note>();
        public ObservableCollection<Note> DoneNotesList { get; set; } = new ObservableCollection<Note>();
        public Category tmpCategory { get; set; }

        //public string SelectedCategoryName { get; set; } = "All";

        public MainWindow()
        {
            InitializeComponent();
            
            GetAllNotes();
            GetAllCategories();

            SelectedCategoryTextBlock.Text = "All";
        }



        public async void GetAllNotes()
        {
            var tmp = await _noteService.GetAllAsync();

            var notes = tmp.Where(n => n.IsDone == false).ToList();
            this.NotesList = new ObservableCollection<Note>(notes);
            NotesListBox.ItemsSource = NotesList;

            
            var doneNotes = tmp.Where(n => n.IsDone == true).ToList();
            DoneNotesList = new ObservableCollection<Note>(doneNotes);
            DoneNotesListBox.ItemsSource = DoneNotesList;
        }
        private async void GetAllCategories()
        {
            var tmp = await _categoryService.GetAllAsync();
            Categories = new ObservableCollection<Category>(tmp);

            CategoriesListBox.ItemsSource = Categories;
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
        private void AddNote(object sender, RoutedEventArgs e)
        {
            AddNoteFormWindow w = new AddNoteFormWindow();
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;       
            w.ShowDialog();

            GetAllNotes();
        }

        private async void DeleteNoteAsync(Note note)
        {
            await _noteService.DeleteAsync(note);

            GetAllNotes();
        }

        //żeby usunąc notetke trzeba najpierw zaznaczyc element w listboxie a potem nacisnąc przycisk
        private void delNote(object sender, RoutedEventArgs e)
        {
            Note note = NotesListBox.SelectedItem as Note;

            NotesList.Remove(note);
            DeleteNoteAsync(note);

            
            //this.NotesList.RemoveAt(0);

        }

        private void delNoteDone(object sender, RoutedEventArgs e)
        {
            Note note = DoneNotesListBox.SelectedItem as Note;

            NotesList.Remove(note);
            DeleteNoteAsync(note);


            //this.NotesList.RemoveAt(0);

        }

        private async void GetByCategory(object sender, RoutedEventArgs e)
        {
            Category category = CategoriesListBox.SelectedItem as Category;

            var tmp = await _noteService.GetNotesByCategoryIdAsync(category.Id);

            tmp = tmp.Where(n => n.IsDone == false).ToList();
            NotesList = new ObservableCollection<Note>(tmp);
            NotesListBox.ItemsSource = NotesList;

            SelectedCategoryTextBlock.Text = category.Name;
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
            SelectedCategoryTextBlock.Text = "All";
        }

        public void ShowDetails(object sender, RoutedEventArgs e)
        {
            Note note = NotesListBox.SelectedItem as Note;
            GetCategoryById(note.CategoryId);

            EditNoteFormWindow w = new EditNoteFormWindow(note, tmpCategory);
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowDialog();

            GetAllNotes();

        }

        public void ShowDetailsDone(object sender, RoutedEventArgs e)
        {
            Note note = DoneNotesListBox.SelectedItem as Note;
            GetCategoryById(note.CategoryId);

            EditNoteFormWindow w = new EditNoteFormWindow(note, tmpCategory);
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowDialog();

            GetAllNotes();

        }

        private async void GetCategoryById(int id)
        {
            tmpCategory = await _categoryService.GetByIdAsync(id);
            
        }

        private void CategoriesBtnClick(object sender, RoutedEventArgs e)
        {
            CategoriesWindow w = new CategoriesWindow();
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowDialog();

            GetAllCategories();
        }

        private async void eNote(Note note)
        {
            await _noteService.UpdateAsync(note);
        }

        private void NoteDone(object sender, RoutedEventArgs e)
        {
            Note note = NotesListBox.SelectedItem as Note;
            note.IsDone = true;

            NotesList.Remove(note);
            DoneNotesList.Add(note);

            eNote(note);

           // GetAllNotes();

        }

        private void NoteUndone(object sender, RoutedEventArgs e)
        {
            Note note = DoneNotesListBox.SelectedItem as Note;
            note.IsDone = false;

            NotesList.Add(note);
            DoneNotesList.Remove(note);

            eNote(note);
        }
    }

    
}
