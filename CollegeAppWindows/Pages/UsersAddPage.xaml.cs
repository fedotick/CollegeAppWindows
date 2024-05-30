using CollegeAppWindows.Models;
using CollegeAppWindows.Services;
using CollegeAppWindows.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for UsersAddPage.xaml
    /// </summary>
    public partial class UsersAddPage : Page
    {
        private UserService userService = UserService.GetInstance;
        private RoleService roleService = RoleService.GetInstance;
        private TeacherService teacherService = TeacherService.GetInstance;

        private User? user = null;

        public UsersAddPage()
        {
            InitializeComponent();
            InitializeComboBoxRole();
            InitializeComboBoxTeacher();
            btn.Click += BtnAdd_Click;
            btn.Content = "Add";
        }

        public UsersAddPage(User user)
        {
            InitializeComponent();
            btn.Click += BtnUpdate_Click;
            btn.Content = "Update";

            this.user = user;
            FillFields();
        }

        private void InitializeComboBoxRole()
        {
            List<Role> roles = new List<Role>();

            roles.Add(new Role { Id = 0, Name = "Select role" });

            roles.AddRange(roleService.GetAll());

            comboBoxRole.SelectedValuePath = "Id";
            comboBoxRole.DisplayMemberPath = "Name";
            comboBoxRole.ItemsSource = roles;

            comboBoxRole.SelectedIndex = 0;
        }

        private void InitializeComboBoxTeacher()
        {
            List<Teacher> teachers = new List<Teacher>();

            teachers.Add(new Teacher { Id = 0, FullName = "Select teacher" });

            teachers.AddRange(teacherService.GetAll());

            comboBoxTeacher.SelectedValuePath = "Id";
            comboBoxTeacher.DisplayMemberPath = "FullName";
            comboBoxTeacher.ItemsSource = teachers;

            comboBoxTeacher.SelectedIndex = 0;
        }

        private void FillFields()
        {
            textBoxUsername.Text = user.Username;

            stackPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            User user = GetUSerFromFields();

            userService.Add(user);

            UsersShow();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            User user = this.user;

            user.Username = textBoxUsername.Text;
            if (textBoxPassword.Text != "")
            {
                user.Password = textBoxPassword.Text;
            }

            userService.Update(user);

            UsersShow();
        }

        private User GetUSerFromFields()
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            int roleId = Convert.ToInt32(comboBoxRole.SelectedValue);
            int teacherId = Convert.ToInt32(comboBoxTeacher.SelectedValue);

            User user = new User
            {
                Username = username,
                Password = password,
                RoleId = roleId,
                TeacherId = teacherId
            };

            return user;
        }

        private void TextBoxUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxUsername, textBlockUsername, nameof(User.Username));
        }

        private void TextBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxPassword, textBlockPassword, nameof(User.Password));
        }

        private void ComboBoxRole_LostFocus(object sender, RoutedEventArgs e)
        {
            if (comboBoxRole.SelectedIndex == 0)

            {
                textBlockRole.Text = "Role is required!";
            }
            else
            {
                textBlockRole.Text = "";
            }
        }

        private void ComboBoxTeacher_LostFocus(object sender, RoutedEventArgs e)
        {
            if (comboBoxTeacher.SelectedIndex == 0)

            {
                textBlockTeacher.Text = "Teacher is required!";
            }
            else
            {
                textBlockTeacher.Text = "";
            }
        }

        private void ValidateTextBox(TextBox textBox, TextBlock textBlock, string propertyName)
        {
            string? text = textBox.Text != "" ? textBox.Text : null;

            List<string> errors = new List<string>();

            switch (propertyName)
            {
                case nameof(User.Username):
                    errors = ValidationUtil.ValidateProperty(new User { Username = text }, propertyName);
                    break;
                case nameof(User.Password):
                    errors = ValidationUtil.ValidateProperty(new User { Password = text }, propertyName);
                    break;
            }

            if (errors.Any())
            {
                textBlock.Text = string.Join("\n", errors);
            }
            else
            {
                textBlock.Text = "";
            }
        }

        private void UsersShow()
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);

            while (!(parentObject is Frame))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);
            }

            if (parentObject is Frame contentFrame)
            {
                contentFrame.Navigate(new Uri("Pages/UsersShowPage.xaml", UriKind.Relative));
            }
        }
    }
}
