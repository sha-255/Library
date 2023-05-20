using Library.Common;
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
            Initialized += (s, e) => Settings.Apply();
            InitializeComponent();
            MainFrame.Navigate(new Readers());
        }
    }
}
