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
    /// Interaction logic for StudentsAddPage.xaml
    /// </summary>
    public partial class StudentsAddPage : Page
    {
        private GroupService groupService = GroupService.GetInstance;
        private StudentService studentService = StudentService.GetInstance;

        public StudentsAddPage()
        {
            InitializeComponent();
            InitializeComboBoxGroup();
            btnAdd.Click += BtnAdd_Click;
        }

        private void InitializeComboBoxGroup()
        {
            List<Group> groupList = new List<Group>();

            groupList.Add(new Group { Id = 0, Name = "Select group" });

            groupList.AddRange(groupService.GetAll());

            comboBoxGroup.SelectedValuePath = "Id";
            comboBoxGroup.DisplayMemberPath = "Name";
            comboBoxGroup.ItemsSource = groupList;

            comboBoxGroup.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string fullName = textBoxFullName.Text;
            string idnp = textBoxIDNP.Text;
            int groupId = Convert.ToInt32(comboBoxGroup.SelectedValue);
            byte? subgroupNumber = null;
            try
            {
                subgroupNumber = Convert.ToByte(textBoxSubgroupNumber.Text);
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            short? cardNumber = null;
            try
            {
                cardNumber = Convert.ToInt16(textBoxCardNumber.Text);
            }
            catch(Exception ex)
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

            Student student = new Student
            {
                FullName = fullName,
                IDNP = idnp,
                GroupId = groupId,
                SubgroupNumber = subgroupNumber,
                CardNumber = cardNumber,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email
            };

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

            StudentAddress studentAddress = new StudentAddress
            {
                Region = region,
                City = city,
                Street = street,
                HouseNumber = houseNumber,
                ApartmentNumber = apartmentNumber
            };

            studentService.Add(student, studentAddress);

            StudentsShow();
        }

        private void StudentsShow()
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);

            while (!(parentObject is Frame))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);
            }

            if (parentObject is Frame contentFrame)
            {
                contentFrame.Navigate(new Uri("Pages/StudentsShowPage.xaml", UriKind.Relative));
            }
        }
    }
}
