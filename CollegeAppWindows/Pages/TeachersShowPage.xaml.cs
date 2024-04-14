using CollegeAppWindows.Models;
using CollegeAppWindows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Filter properties
        private HashSet<string> specificCathedraNames = new HashSet<string>();
        private int minExperience = int.MinValue;
        private int maxExperience = int.MaxValue;
        private HashSet<string> specificRegions = new HashSet<string>();
        private HashSet<string> specificCities = new HashSet<string>();
        private HashSet<string> specificStreets = new HashSet<string>();

        public TeachersShowPage()
        {
            InitializeComponent();

            LoadTeachers();

            btnAddNewEntry.Click += BtnAddNewEntry_Click;
        }

        private void LoadTeachers()
        {
            List<TeacherView> teacherViews = teacherViewService.GetAll();
            var filteredTeachers = FilterTeachers(teacherViews);
            dataGrid.ItemsSource = filteredTeachers;
        }

        private IEnumerable<TeacherView> FilterTeachers(IEnumerable<TeacherView> teachers)
        {
            return teachers.Where(FilterTeacherView);
        }

        private bool FilterTeacherView(TeacherView teacherView)
        {
            return specificCathedraNames.Contains(teacherView.CathedraName) 
                && teacherView.Experience >= minExperience 
                && teacherView.Experience <= maxExperience 
                && specificRegions.Contains(teacherView.Region) 
                && specificCities.Contains(teacherView.City)
                && specificStreets.Contains(teacherView.Street);
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
