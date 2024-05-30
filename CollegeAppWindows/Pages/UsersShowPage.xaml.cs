using CollegeAppWindows.Models;
using CollegeAppWindows.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for UsersShowPage.xaml
    /// </summary>
    public partial class UsersShowPage : Page
    {
        private UserService userService = UserService.GetInstance;

        private List<User> users;

        public UsersShowPage()
        {
            InitializeComponent();

            users = userService.GetAll();

            btnAddNewEntry.Click += BtnAddNewEntry_Click;

            ShowUsers();
        }

        private void BtnAddNewEntry_Click(object sender, RoutedEventArgs e)
        {
            Frame parentFrame = GetParentFrame(sender as DependencyObject);

            parentFrame.Navigate(new Uri("Pages/UsersAddPage.xaml", UriKind.Relative));
        }

        private void ContextMenuEdit_Click(object sender, RoutedEventArgs e)
        {
            User user = dataGrid.SelectedItem as User;

            if (user != null)
            {
                Frame parentFrame = GetParentFrame(dataGrid);

                parentFrame.Navigate(new UsersAddPage(user));
            }
        }

        private void ContextMenuDelete_Click(object sender, RoutedEventArgs e)
        {
            User? user = dataGrid.SelectedItem as User;

            if (user != null)
            {
                try
                {
                    userService.Delete(user.Id);
                    users.Remove(user);
                    ShowUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "UserShowPage.xaml.cs | ContextMenuDelete_Click");
                }

            }
        }

        private void ShowUsers()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = users;
        }

        private Frame GetParentFrame(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (!(parent is Frame))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as Frame;
        }
    }
}
