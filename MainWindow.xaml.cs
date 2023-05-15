using AdvertisingAgency.Common;
using Library.Pages;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Readers());
            Initialized += (s, e) => Settings.Apply();
        }
    }
}
