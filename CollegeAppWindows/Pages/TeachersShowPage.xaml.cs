using CollegeAppWindows.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for TeachersShowPage.xaml
    /// </summary>
    public partial class TeachersShowPage : Page
    {
        private TeacherViewService teacherViewService = new TeacherViewService();

        public TeachersShowPage()
        {
            InitializeComponent();

            LoadTeachers();

            btnAddNewEntry.Click += BtnAddNewEntry_Click;
        }

        private void LoadTeachers()
        {
            dataGrid.ItemsSource = teacherViewService.GetAll();
        }

        private void BtnAddNewEntry_Click(object sender, RoutedEventArgs e)
        {
            Frame parentFrame = GetParentFrame(sender as DependencyObject);

            parentFrame.Navigate(new Uri("Pages/TeachersAddPage.xaml", UriKind.Relative));
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
