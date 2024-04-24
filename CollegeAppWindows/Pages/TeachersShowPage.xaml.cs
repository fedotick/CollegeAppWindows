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
        private TeacherService teacherService = TeacherService.GetInstance;
        private TeacherViewService teacherViewService = TeacherViewService.GetInstance;

        private List<TeacherView> teacherViews;
        private List<TeacherView> filteredTeacherViews;

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
            filteredTeacherViews = teacherViews;

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
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = filteredTeacherViews;
            textBlockEntries.Text = $"Entries: {filteredTeacherViews.Count}";
        }

        private void CheckBox_Changed(object sender, EventArgs e)
        {
            FilterTeacherViews();
        }

        private void FilterTeacherViews()
        {
            filteredTeacherViews = teacherViews;

            List<SelectableItem>? specificCathedraNames = comboBoxCathedra.ItemsSource as List<SelectableItem>;
            HashSet<string> specificCathedraNames1 = SelectableItemUtil.GetCheckedItemsHashSet(specificCathedraNames);
            if (specificCathedraNames1.Count > 0)
            {
                filteredTeacherViews = DataUtil.FilterBySpecificItems(filteredTeacherViews, specificCathedraNames1, t => t.CathedraName);
            }

            List<SelectableItem>? specificRegions = comboBoxRegion.ItemsSource as List<SelectableItem>;
            HashSet<string> specificRegions1 = SelectableItemUtil.GetCheckedItemsHashSet(specificRegions);
            if (specificRegions1.Count > 0)
            {
                filteredTeacherViews = DataUtil.FilterBySpecificItems(filteredTeacherViews, specificRegions1, t => t.Region);
            }

            List<SelectableItem>? specificCities = comboBoxCity.ItemsSource as List<SelectableItem>;
            HashSet<string> specificCities1 = SelectableItemUtil.GetCheckedItemsHashSet(specificCities);
            if (specificCities1.Count > 0)
            {
                filteredTeacherViews = DataUtil.FilterBySpecificItems(filteredTeacherViews, specificCities1, t => t.City);
            }

            List<SelectableItem>? specificStreets = comboBoxStreet.ItemsSource as List<SelectableItem>;
            HashSet<string> specificStreets1 = SelectableItemUtil.GetCheckedItemsHashSet(specificStreets);
            if (specificStreets1.Count > 0)
            {
                filteredTeacherViews = DataUtil.FilterBySpecificItems(filteredTeacherViews, specificStreets1, t => t.Street);
            }

            ShowTeachers();
        }

        private void BtnAddNewEntry_Click(object sender, RoutedEventArgs e)
        {
            Frame parentFrame = GetParentFrame(sender as DependencyObject);

            parentFrame.Navigate(new Uri("Pages/TeachersAddPage.xaml", UriKind.Relative));
        }

        private void ContextMenuEdit_Click(object sender, RoutedEventArgs e)
        {
            TeacherView? teacherView = dataGrid.SelectedItem as TeacherView;

            if (teacherView != null)
            {
                Frame parentFrame = GetParentFrame(dataGrid);

                parentFrame.Navigate(new TeachersAddPage(teacherView));
            }
        }

        private void ContextMenuDelete_Click(object sender, RoutedEventArgs e)
        {
            TeacherView? teacherView = dataGrid.SelectedItem as TeacherView;

            if (teacherView != null)
            {
                teacherService.Delete(teacherView.Id);

                teacherViews.Remove(teacherView);
                FilterTeacherViews();
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
