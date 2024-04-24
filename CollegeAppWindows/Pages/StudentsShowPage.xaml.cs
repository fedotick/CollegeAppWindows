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
    /// Interaction logic for StudentsShowPage.xaml
    /// </summary>
    public partial class StudentsShowPage : Page
    {
        private StudentService studentService = StudentService.GetInstance;
        private StudentViewService studentViewService = StudentViewService.GetInstance;

        private List<StudentView> studentViews;
        private List<StudentView> filteredStudentViews;

        // Filter properties
        private HashSet<string> availableGroupNames = new HashSet<string>();
        //private HashSet<string> availableSubgroupNumbers = new HashSet<string>();
        private HashSet<string> availableRegions = new HashSet<string>();
        private HashSet<string> availableCities = new HashSet<string>();
        private HashSet<string> availableStreets = new HashSet<string>();

        public List<SelectableItem> SpecificGroupNames { get; set; } = new List<SelectableItem>();
        //public List<SelectableItem> SpecificSubgroupNumbers { get; set; } = new List<SelectableItem>();
        public List<SelectableItem> SpecificRegions { get; set; } = new List<SelectableItem>();
        public List<SelectableItem> SpecificCities { get; set; } = new List<SelectableItem>();
        public List<SelectableItem> SpecificStreets { get; set; } = new List<SelectableItem>();

        public StudentsShowPage()
        {
            InitializeComponent();
            DataContext = this;

            studentViews = studentViewService.GetAll();
            filteredStudentViews = studentViews;

            InitializeComboBoxes();
            ShowStudents();

            btnAddNewEntry.Click += BtnAddNewEntry_Click;
        }

        private void InitializeComboBoxes()
        {
            InitializeUniqueValues();

            SelectableItemUtil.AddTextToSelectableItems(availableGroupNames, SpecificGroupNames);
            //SelectableItemUtil.AddTextToSelectableItems(availableSubgroupNumbers, SpecificSubgroupNumbers);
            SelectableItemUtil.AddTextToSelectableItems(availableRegions, SpecificRegions);
            SelectableItemUtil.AddTextToSelectableItems(availableCities, SpecificCities);
            SelectableItemUtil.AddTextToSelectableItems(availableStreets, SpecificStreets);
        }

        private void InitializeUniqueValues()
        {
            availableGroupNames = DataUtil.GetUniqueValues(studentViews, t => t.GroupName);
            //availableSubgroupNumbers = DataUtil.GetUniqueValues(studentViews, t => t.SubgroupNumber);
            availableRegions = DataUtil.GetUniqueValues(studentViews, t => t.Region);
            availableCities = DataUtil.GetUniqueValues(studentViews, t => t.City);
            availableStreets = DataUtil.GetUniqueValues(studentViews, t => t.Street);
        }

        private void ShowStudents()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = filteredStudentViews;
        }

        private void CheckBox_Changed(object sender, EventArgs e)
        {
            FilterStudentViews();
        }

        private void FilterStudentViews()
        {
            filteredStudentViews = studentViews;

            List<SelectableItem>? specificCathedraNames = comboBoxCathedra.ItemsSource as List<SelectableItem>;
            HashSet<string> specificCathedraNames1 = SelectableItemUtil.GetCheckedItemsHashSet(specificCathedraNames);
            if (specificCathedraNames1.Count > 0)
            {
                filteredStudentViews = DataUtil.FilterBySpecificItems(filteredStudentViews, specificCathedraNames1, t => t.GroupName);
            }

            List<SelectableItem>? specificRegions = comboBoxRegion.ItemsSource as List<SelectableItem>;
            HashSet<string> specificRegions1 = SelectableItemUtil.GetCheckedItemsHashSet(specificRegions);
            if (specificRegions1.Count > 0)
            {
                filteredStudentViews = DataUtil.FilterBySpecificItems(filteredStudentViews, specificRegions1, t => t.Region);
            }

            List<SelectableItem>? specificCities = comboBoxCity.ItemsSource as List<SelectableItem>;
            HashSet<string> specificCities1 = SelectableItemUtil.GetCheckedItemsHashSet(specificCities);
            if (specificCities1.Count > 0)
            {
                filteredStudentViews = DataUtil.FilterBySpecificItems(filteredStudentViews, specificCities1, t => t.City);
            }

            List<SelectableItem>? specificStreets = comboBoxStreet.ItemsSource as List<SelectableItem>;
            HashSet<string> specificStreets1 = SelectableItemUtil.GetCheckedItemsHashSet(specificStreets);
            if (specificStreets1.Count > 0)
            {
                filteredStudentViews = DataUtil.FilterBySpecificItems(filteredStudentViews, specificStreets1, t => t.Street);
            }

            ShowStudents();
        }

        private void BtnAddNewEntry_Click(object sender, RoutedEventArgs e)
        {
            Frame parentFrame = GetParentFrame(sender as DependencyObject);

            parentFrame.Navigate(new Uri("Pages/StudentsAddPage.xaml", UriKind.Relative));
        }

        private void ContextMenuEdit_Click(object sender, RoutedEventArgs e)
        {
            StudentView? studentView = dataGrid.SelectedItem as StudentView;

            if (studentView != null)
            {
                Frame parentFrame = GetParentFrame(dataGrid);

                // parentFrame.Navigate(new StudentAddPage(studentView));
            }
        }

        private void ContextMenuDelete_Click(object sender, RoutedEventArgs e)
        {
            StudentView? studentView = dataGrid.SelectedItem as StudentView;

            if (studentView != null)
            {
                studentService.Delete(studentView.Id);

                studentViews.Remove(studentView);
                FilterStudentViews();
            }
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
