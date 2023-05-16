using Library.Domain.Context;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для Readers.xaml
    /// </summary>
    public partial class Readers : Page
    {
        ObservableCollection<Reader> readers = new ObservableCollection<Reader>() 
        {
            new Reader()
            {
                Id = 1,
                Name = "Foo",
                RecordingDate = DateTime.Now,
            },
            new Reader()
            {
                Id = 2,
                Name = "Bar",
                RecordingDate = DateTime.Now,
            },
            new Reader()
            {
                Id = 3,
                Name = "Som",
                RecordingDate = DateTime.Now,
            },
        };

        ReadersContext readersContext = new();

        public Readers()
        {
            InitializeComponent();
            //ApplyContext();
            ReaderView.ItemsSource = readers;
            SearchView.ItemsSource = readers;
        }

        private async void ApplyContext() => ReaderView.ItemsSource = await readersContext.Readers.ToListAsync();
    }
}
