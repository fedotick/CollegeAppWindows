using System;
using System.Windows.Controls;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        public TeachersPage()
        {
            InitializeComponent();
           
            ContentFrame.Navigate(new Uri("Pages/TeachersShowPage.xaml", UriKind.Relative));
        }
    }
}
