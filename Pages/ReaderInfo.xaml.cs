using Library.Domain.Context;
using Library.Domain.Data;
using Library.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library.Pages
{
    /// <summary>
    /// Interaction logic for ReaderInfo.xaml
    /// </summary>
    public partial class ReaderInfo : Page
    {
        BooksContext BooksContext = new();
        ReadersContext readersContext = new();
        Reader CurrentReader;
        ObservableCollection<BookDto> readerBooks = new();

        public ReaderInfo(int readerId)
        {
            InitializeComponent();
            CurrentReader = readersContext.Readers.Find(readerId);
            Management();
        }

        private void Management()
        {
            OpenReaders.Click += (s, e) => NavigationService.Navigate(new Readers());
            GenerateReaderBooks();
            FillInput();
            ApplyContext();
            Add.Click += OnAddClick;
            Remove.Click += OnRemoveClick;
            Apply.Click += OnApplyClick;
            ApplyAndExit.Click += (s, e) => 
            {
                OnApplyClick(s,e);
                NavigationService.Navigate(new Readers());
            };
            Return.Click += OnReturnClick;
        }

        private void OnReturnClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ReturnId.Text, out var index))
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                readerBooks.First(el => el.Id == index).ReturnDate = DateTime.Now.ToString();
                ApplyContext();
            }
            catch
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                ReturnId.Clear();
            }
        }

        private void OnRemoveClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(RemoveId.Text, out var index))
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                readerBooks.Remove(readerBooks.First(el => el.Id == index));
                ApplyContext();
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

        private void OnApplyClick(object sender, RoutedEventArgs e)
        {
            ApplyChanges();
            readersContext.Readers.Entry(CurrentReader).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            readersContext.SaveChanges();
        }

        private void ApplyChanges()
        {
            CurrentReader.Birthday = DateTime.Parse(PostBirthday.Text);
            CurrentReader.Group = PostGroup.Text;
            CurrentReader.Name = PostName.Text;
            CurrentReader.AddressAndPhone = PostAddressAndPhone.Text;
            CurrentReader.RecordingDate = DateTime.Parse(PostRecordingDate.Text);
            CurrentReader.Books = JsonConvert.SerializeObject(readerBooks);
        }

        private void ApplyContext()
        {
            BooksListBox.ItemsSource = readerBooks;
            BooksListBox.Items.Refresh();
        }

        private void GenerateReaderBooks()
        {
            try
            {
                readerBooks = JsonConvert.DeserializeObject<ObservableCollection<BookDto>>(CurrentReader.Books);
            }
            catch
            {
                CurrentReader.Books = JsonConvert.SerializeObject(readerBooks);
            }
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AddId.Text, out var index))
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var book = BooksContext.Books.Find(index);
                var bookDto = new BookDto() 
                {
                    Id = book.Id,
                    InventoryNumber = book.InventoryNumber,
                    NameAndAuthor = book.NameAndAuthor,
                };
                readerBooks.Add(bookDto);
                ApplyContext();
            }
            catch
            {
                MessageBox.Show("Неверные данные", "Библиотека", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                AddId.Clear();
            }
        }

        private void FillInput()
        {
            PostBirthday.Text = CurrentReader.Birthday.ToString();
            PostGroup.Text = CurrentReader.Group;
            PostName.Text = CurrentReader.Name;
            PostAddressAndPhone.Text = CurrentReader.AddressAndPhone;
            PostRecordingDate.Text = CurrentReader.RecordingDate.ToString();
        }
    }
}
