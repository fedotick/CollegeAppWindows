using CollegeAppWindows.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private UserService userService = UserService.GetInstance;

        public LogInPage()
        {
            InitializeComponent();

            btnLogin.Click += BtnLogin_ClicK;
        }

        private void BtnLogin_ClicK(object sender, RoutedEventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = passwordBoxPassword.Password;

            if(userService.LogIn(username, password))
            {
                ShowMainPage();
            }
            else
            {
                MessageBox.Show("Incorrect login or password!");
            }
        }

        private void ShowMainPage()
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);

            while (!(parentObject is Frame))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);
            }

            if (parentObject is Frame contentFrame)
            {
                contentFrame.Navigate(new Uri("Pages/MainPage.xaml", UriKind.Relative));
            }
        }
    }
}
