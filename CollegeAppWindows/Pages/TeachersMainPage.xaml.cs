using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Логика взаимодействия для TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        public TeachersPage()
        {
            InitializeComponent();
           
            ContentFrame.Navigate(new Uri("Pages/TeachersShowPage.xaml", UriKind.Relative));

            btnAddNewEntry.Click += BtnAddNewEntry_Click;
        }

        private void BtnAddNewEntry_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Uri("Pages/TeachersAddPage.xaml", UriKind.Relative));
        }
    }
}
