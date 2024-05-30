using System;
using System.Windows.Controls;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for UsersMainPage.xaml
    /// </summary>
    public partial class UsersMainPage : Page
    {
        public UsersMainPage()
        {
            InitializeComponent();

            ContentFrame.Navigate(new Uri("Pages/UsersShowPage.xaml", UriKind.Relative));
        }
    }
}
