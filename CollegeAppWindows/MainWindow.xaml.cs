using System;
using System.Windows;

namespace CollegeAppWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        }
    }
}
