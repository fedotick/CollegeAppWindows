using CollegeAppWindows.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private User user;

        public MainPage()
        {
            InitializeComponent();

            user = LoggedInUser.GetInstance().GetUser();

            btnCathedrae.Click += Btn_Click;
            btnGroups.Click += Btn_Click;
            btnSections.Click += Btn_Click;
            btnSpecialties.Click += Btn_Click;
            btnStudents.Click += Btn_Click;
            btnTeachers.Click += Btn_Click;
            btnUsers.Click += Btn_Click;

            //textBlockLogOut.MouseLeftButtonDown += TextBlockLogOut_MouseLeftButtonDown;

            CheckRole();
        }

        private void CheckRole()
        {
            if (user.RoleId == 2)
            {
                btnUsers.Visibility = Visibility.Collapsed;
            }

            if (user.RoleId == 3)
            {
                btnCathedrae.Visibility = Visibility.Collapsed;
                btnSections.Visibility = Visibility.Collapsed;
                btnSpecialties.Visibility = Visibility.Collapsed;
                btnTeachers.Visibility = Visibility.Collapsed;
                btnUsers.Visibility = Visibility.Collapsed;
            }

            if (user.RoleId == 4)
            {
                btnCathedrae.Visibility = Visibility.Collapsed;
                btnGroups.Visibility = Visibility.Collapsed;
                btnSections.Visibility = Visibility.Collapsed;
                btnSpecialties.Visibility = Visibility.Collapsed;
                btnTeachers.Visibility = Visibility.Collapsed;
                btnUsers.Visibility = Visibility.Collapsed;
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button) 
            {
                string buttonContent = button.Content.ToString();

                ContentFrame.Navigate(new Uri($"Pages/{buttonContent}MainPage.xaml", UriKind.Relative));
            }
        }

        private void TextBlockLogOut_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you really want to log out?", "Log Out!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                ShowLogInPage();
            }
        }

        private void ShowLogInPage()
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);

            while (!(parentObject is Frame))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);
            }

            if (parentObject is Frame contentFrame)
            {
                contentFrame.Navigate(new Uri("Pages/LogInPage.xaml", UriKind.Relative));
            }
        }
    }
}
