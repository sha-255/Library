using Library.Domain.Data;
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

        public Readers()
        {
            InitializeComponent();
            ReaderView.ItemsSource = readers;
        }
    }
}
