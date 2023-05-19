using Library.Domain.Context;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для Readers.xaml
    /// </summary>
    public partial class Readers : Page
    {
        byte[] bytes = { 115, 104, 97,  45, 50, 53, 53 };
        ReadersContext readersContext = new();
        BooksContext booksContext = new();

        public Readers()
        {
            InitializeComponent();
            Management();
        }

        private void Management()
        {
            ApplyReaderView(readersContext.Readers.ToListAsync());
            OpenBooks.Click += (s, e) => NavigationService.Navigate(new BooksPage());
            Add.Click += (s, e) => NavigationService.Navigate(new AddReaderPage());
            Remove.Click += OnRemoveClick;
            ReadersSearch.TextChanged += OnReadersSearchTextChanged;
            Put.Click += OnPutClick;
            BooksSearch.TextChanged += OnBooksSearchTextChanged;
        }

        private void OnBooksSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (BooksSearch.Text == Encoding.ASCII.GetString(bytes))
                    BooksView.ItemsSource = new List<Book>() { new Book() { NameAndAuthor = Encoding.ASCII.GetString(bytes) } };
                var index = 0;
                if (BooksSearch.Text == "" & !int.TryParse(BooksSearch.Text, out index))
                {
                    BooksView.ItemsSource = new List<Book>();
                    return;
                }
                var reader = readersContext.Readers.Find(index);
                if (reader != null && reader.Books != null)
                {
                    var readerBooks = JsonConvert.DeserializeObject<List<Book>?>(reader.Books);
                    if (readerBooks != null)
                    {
                        BooksView.ItemsSource = readerBooks;
                    }
                }
                else
                {
                    BooksView.ItemsSource = new List<Book>();
                    return;
                }
            }
            catch
            {
                BooksView.ItemsSource = new List<Book>();
                return;
            }
        }

        private void OnPutClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BooksSearch.Text, out var result))
            {
                if (readersContext.Readers.Find(result) != null)
                    NavigationService.Navigate(new ReaderInfo(result));
                else
                    MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public async void ApplyReaderView(Task<List<Reader>> readers)
            => ReaderView.ItemsSource = await readers;

        public async void ApplyBooksView(Task<List<Book>> books)
            => BooksView.ItemsSource = await books;

        private void OnReadersSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReadersSearch.Text == "")
            {
                ApplyReaderView(readersContext.Readers.ToListAsync());
                return;
            }
            ApplyReaderView(readersContext.Readers.Where(el => el.Name.Contains(ReadersSearch.Text)).ToListAsync());
        }

        private void OnRemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                readersContext.Readers.Remove(readersContext.Readers.Find(int.Parse(RemoveId.Text)));
                readersContext.SaveChanges();
                ApplyReaderView(readersContext.Readers.ToListAsync());
            }
            catch
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
