using System;
using System.Windows;
using System.Windows.Controls;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
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
