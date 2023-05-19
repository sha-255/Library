using Library.Domain.Context;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ApplyContext(booksContext.Books.ToListAsync());
            Add.Click += OnAddClick;
            Remove.Click += OnRemoveClick;
            BooksSearch.TextChanged += OnBooksSearchTextChanged;
        }

        private void OnBooksSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (BooksSearch.Text == "")
            {
                ApplyContext(booksContext.Books.ToListAsync());
                return;
            }
            ApplyContext(booksContext.Books.Where(el => el.NameAndAuthor.Contains(BooksSearch.Text)).ToListAsync());
        }

        private void OnRemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                booksContext.Books.Remove(booksContext.Books.Find(Math.Abs(int.Parse(RemoveId.Text))));
                booksContext.SaveChanges();
                ApplyContext(booksContext.Books.ToListAsync());
            }
            catch
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                RemoveId.Clear();
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
                ApplyContext(booksContext.Books.ToListAsync());
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

        public async void ApplyContext(Task<List<Book>> books)
            => BooksListBox.ItemsSource = await books;
    }
}
