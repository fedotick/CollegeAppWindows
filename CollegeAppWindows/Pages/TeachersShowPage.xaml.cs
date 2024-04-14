using CollegeAppWindows.Models;
using CollegeAppWindows.Services;
using CollegeAppWindows.Utilities;
using System;
using System.Collections.Generic;
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
        private List<TeacherView> teacherViews;

        // Filter properties
        private HashSet<string> availableCathedraNames = new HashSet<string>();
        private int minExperience = int.MinValue;
        private int maxExperience = int.MaxValue;
        private HashSet<string> availableRegions = new HashSet<string>();
        private HashSet<string> availableCities = new HashSet<string>();
        private HashSet<string> availableStreets = new HashSet<string>();

        public List<SelectableItem> SpecificCathedraNames { get; set; } = new List<SelectableItem>();
        public List<SelectableItem> SpecificRegions { get; set; } = new List<SelectableItem>();
        public List<SelectableItem> SpecificCities { get; set; } = new List<SelectableItem>();
        public List<SelectableItem> SpecificStreets { get; set; } = new List<SelectableItem>();

        public TeachersShowPage()
        {
            InitializeComponent();
            DataContext = this;

            teacherViews = teacherViewService.GetAll();

            InitializeComboBoxes();
            ShowTeachers();

            btnAddNewEntry.Click += BtnAddNewEntry_Click;
        }

        private void InitializeComboBoxes()
        {
            InitializeUniqueValues();

            SelectableItemUtil.AddTextToSelectableItems(availableCathedraNames, SpecificCathedraNames);
            SelectableItemUtil.AddTextToSelectableItems(availableRegions, SpecificRegions);
            SelectableItemUtil.AddTextToSelectableItems(availableCities, SpecificCities);
            SelectableItemUtil.AddTextToSelectableItems(availableStreets, SpecificStreets);
        }

        private void InitializeUniqueValues()
        {
            availableCathedraNames = DataUtil.GetUniqueValues(teacherViews, t => t.CathedraName);
            availableRegions = DataUtil.GetUniqueValues(teacherViews, t => t.Region);
            availableCities = DataUtil.GetUniqueValues(teacherViews, t => t.City);
            availableStreets = DataUtil.GetUniqueValues(teacherViews, t => t.Street);
        }

        private void ShowTeachers()
        {
            dataGrid.ItemsSource = teacherViews;
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
