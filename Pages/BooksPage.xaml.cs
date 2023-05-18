using Library.Domain.Context;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        BooksContext booksContext = new();

        public BooksPage()
        {
            InitializeComponent();
            OpenReaders.Click += (s, e) => NavigationService.Navigate(new Pages.Readers());
            ApplyContext();
            Add.Click += OnAddClick;
            Remove.Click += OnRemoveClick;
        }

        private void OnRemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                booksContext.Books.Remove(booksContext.Books.Find(Math.Abs(int.Parse(RemoveId.Text))));
            }
            catch
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var book = new Book() 
                {
                    InventoryNumber = Math.Abs(int.Parse(AddInventoryNumber.Text)),
                    NameAndAuthor = AddNameAndAuthor.Text,
                };
                booksContext.Books.Add(book);
                booksContext.SaveChanges();
                ApplyContext();
            }
            catch
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                AddInventoryNumber.Clear();
                AddNameAndAuthor.Clear();
            }
        }

        public async void ApplyContext()
            => BooksListBox.ItemsSource = await booksContext.Books.ToListAsync();
    }
}
