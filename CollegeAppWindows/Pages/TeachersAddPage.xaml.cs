using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CollegeAppWindows.Services;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Логика взаимодействия для TeachersAddPage.xaml
    /// </summary>
    public partial class TeachersAddPage : Page
    {
        private Repository<Cathedra> cathedraRepository = Repository<Cathedra>.GetInstance;
        private TeacherService teacherService;

        public TeachersAddPage()
        {
            InitializeComponent();

            teacherService = new TeacherService();

            InitializeComboBoxCathedra();

            btnAdd.Click += BtnAdd_Click;
        }

        private void InitializeComboBoxCathedra()
        {
            List<Cathedra> cathedraList = new List<Cathedra>();

            cathedraList.Add(new Cathedra { Id = 0, Name = "Select cathedra" });

            cathedraList.AddRange(cathedraRepository.GetAll());

            comboBoxCathedra.SelectedValuePath = "Id";
            comboBoxCathedra.DisplayMemberPath = "Name";
            comboBoxCathedra.ItemsSource = cathedraList;

            comboBoxCathedra.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
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

            string region = textBoxRegion.Text;
            string city = textBoxCity.Text;
            string street = textBoxStreet.Text;
            string houseNumber = textBoxHouseNumber.Text;
            int? apartmentNumber = null;
            try
            {
                apartmentNumber = Convert.ToInt32(textBoxApartmentNumber.Text);
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

            teacherService.Add(teacher, teacherAddress);

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
