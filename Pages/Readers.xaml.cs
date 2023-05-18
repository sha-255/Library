using Library.Domain.Context;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        }

        private void OnPutClick(object sender, RoutedEventArgs e)
        {
            
        }

        public async void ApplyReaderView(Task<List<Reader>> readers)
            => ReaderView.ItemsSource = await readers;

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
