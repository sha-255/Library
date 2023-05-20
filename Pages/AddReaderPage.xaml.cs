using Library.Domain.Context;
using Library.Domain.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddReaderPage.xaml
    /// </summary>
    public partial class AddReaderPage : Page
    {
        ReadersContext readersContext = new();  

        public AddReaderPage()
        {
            InitializeComponent();
            Back.Click += (s, e) => NavigationService.GoBack();
            Add.Click += OnAddClick;
        }

        private void OnAddClick(object sender, System.Windows.RoutedEventArgs e)
        {
            AddReader();
            NavigationService.Navigate(new Readers());
        }

        private void AddReader()
        {
            try
            {
                var el = new Reader()
                {
                    Name = AddName.Text,
                    Birthday = DateTime.Parse(AddBirthday.Text),
                    AddressAndPhone = AddAddressAndPhone.Text,
                    Group = AddGroup.Text,
                };
                if (AddRecordingDate.Text == "")
                    el.RecordingDate = DateTime.Now;
                else
                    el.RecordingDate = DateTime.Parse(AddRecordingDate.Text);
                readersContext.Readers.Add(el);
                readersContext.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Неверные данные","Библиотека",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
    }
}
