using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            btnCathedrae.Click += Btn_Click;
            btnGroups.Click += Btn_Click;
            btnSections.Click += Btn_Click;
            btnSpecialties.Click += Btn_Click;
            btnStudents.Click += Btn_Click;
            btnTeachers.Click += Btn_Click;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button) 
            {
                string buttonContent = button.Content.ToString();

                ContentFrame.Navigate(new Uri($"Pages/{buttonContent}MainPage.xaml", UriKind.Relative));
            }
        }
    }
}
