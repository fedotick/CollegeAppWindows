using CollegeAppWindows.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CollegeAppWindows.Services;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Interaction logic for TeachersAddPage.xaml
    /// </summary>
    public partial class TeachersAddPage : Page
    {
        private CathedraService cathedraService = CathedraService.GetInstance;
        private TeacherService teacherService = TeacherService.GetInstance;

        private TeacherView? teacherView = null;

        public TeachersAddPage()
        {
            InitializeComponent();
            InitializeComboBoxCathedra();
            btn.Click += BtnAdd_Click;
            btn.Content = "Add";
        }

        public TeachersAddPage(TeacherView teacherView)
        {
            InitializeComponent();
            InitializeComboBoxCathedra();
            btn.Click += BtnUpdate_Click;
            btn.Content = "Update";

            this.teacherView = teacherView;
            FillFields();
        }

        private void InitializeComboBoxCathedra()
        {
            List<Cathedra> cathedraList = new List<Cathedra>();

            cathedraList.Add(new Cathedra { Id = 0, Name = "Select cathedra" });

            cathedraList.AddRange(cathedraService.GetAll());

            comboBoxCathedra.SelectedValuePath = "Id";
            comboBoxCathedra.DisplayMemberPath = "Name";
            comboBoxCathedra.ItemsSource = cathedraList;

            comboBoxCathedra.SelectedIndex = 0;
        }

        private void FillFields()
        {
            textBoxFullName.Text = teacherView.FullName;
            comboBoxCathedra.SelectedValue = teacherView.CathedraId;
            textBoxExperience.Text = teacherView.Experience.ToString();
            textBoxDateOfBirth.Text = teacherView.DateOfBirth?.ToString("dd.MM.yyyy");
            textBoxPhoneNumber.Text = teacherView.PhoneNumber;
            textBoxEmail.Text = teacherView.Email;

            textBoxRegion.Text = teacherView.Region;
            textBoxCity.Text = teacherView.City;
            textBoxStreet.Text = teacherView.Street;
            textBoxHouseNumber.Text = teacherView.HouseNumber;
            textBoxApartmentNumber.Text = teacherView.ApartmentNumber.ToString();
        }

        private Teacher GetTeacherFromFields()
        {
            string fullName = textBoxFullName.Text;
            int cathedraId = Convert.ToInt32(comboBoxCathedra.SelectedValue);
            int? experience = null;
            try
            {
                experience = Convert.ToInt32(textBoxExperience.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DateTime? dateOfBirth = null;
            try
            {
                dateOfBirth = DateTime.ParseExact(textBoxDateOfBirth.Text, "dd.MM.yyyy", null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string phoneNumber = textBoxPhoneNumber.Text;
            string email = textBoxEmail.Text;

            Teacher teacher = new Teacher
            {
                FullName = fullName,
                CathedraId = cathedraId,
                Experience = experience,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email
            };

            return teacher;
        }

        private TeacherAddress GetTeacherAddressFromFields()
        {
            string region = textBoxRegion.Text;
            string city = textBoxCity.Text;
            string street = textBoxStreet.Text;
            string houseNumber = textBoxHouseNumber.Text;
            short? apartmentNumber = null;
            try
            {
                apartmentNumber = Convert.ToInt16(textBoxApartmentNumber.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            TeacherAddress teacherAddress = new TeacherAddress
            {
                Region = region,
                City = city,
                Street = street,
                HouseNumber = houseNumber,
                ApartmentNumber = apartmentNumber
            };

            return teacherAddress;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Teacher teacher = GetTeacherFromFields();
            TeacherAddress teacherAddress = GetTeacherAddressFromFields();
            
            teacherService.Add(teacher, teacherAddress);

            TeachersShow();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Teacher teacher = GetTeacherFromFields();
            teacher.Id = teacherView.Id;
            teacher.TeacherAddressId = teacherView.TeacherAddressId;

            TeacherAddress teacherAddress = GetTeacherAddressFromFields();
            teacherAddress.Id = teacherView.TeacherAddressId;

            teacherService.Update(teacher, teacherAddress);

            TeachersShow();
        }

        private void TeachersShow()
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);

            while (!(parentObject is Frame))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);
            }

            if (parentObject is Frame contentFrame)
            {
                contentFrame.Navigate(new Uri("Pages/TeachersShowPage.xaml", UriKind.Relative));
            }
        }
    }
}
