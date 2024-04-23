using System;
using System.Windows.Controls;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for StudentsMainPage.xaml
    /// </summary>
    public partial class StudentsMainPage : Page
    {
        public StudentsMainPage()
        {
            InitializeComponent();

            ContentFrame.Navigate(new Uri("Pages/StudentsShowPage.xaml", UriKind.Relative));
        }
    }
}
