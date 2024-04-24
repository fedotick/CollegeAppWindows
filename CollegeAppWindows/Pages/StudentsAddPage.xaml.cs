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

        private StudentView? studentView = null;

        public StudentsAddPage()
        {
            InitializeComponent();
            InitializeComboBoxGroup();
            btn.Click += BtnAdd_Click;
            btn.Content = "Add";
        }

        public StudentsAddPage(StudentView studentView)
        {
            InitializeComponent();
            InitializeComboBoxGroup();
            btn.Click += BtnUpdate_Click;
            btn.Content = "Update";

            this.studentView = studentView;
            FillFields();
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

        private void FillFields()
        {
            textBoxFullName.Text = studentView.FullName;
            textBoxIDNP.Text = studentView.IDNP;
            comboBoxGroup.SelectedValue = studentView.GroupId;
            textBoxSubgroupNumber.Text = studentView.SubgroupNumber.ToString();
            textBoxCardNumber.Text = studentView.CardNumber.ToString();
            textBoxDateOfBirth.Text = studentView.DateOfBirth?.ToString("dd.MM.yyyy");
            textBoxPhoneNumber.Text = studentView.PhoneNumber;
            textBoxEmail.Text = studentView.Email;

            textBoxRegion.Text = studentView.Region;
            textBoxCity.Text = studentView.City;
            textBoxStreet.Text = studentView.Street;
            textBoxHouseNumber.Text = studentView.HouseNumber;
            textBoxApartmentNumber.Text = studentView.ApartmentNumber.ToString();
        }

        private Student GetStudentFromFields()
        {
            string fullName = textBoxFullName.Text;
            string idnp = textBoxIDNP.Text;
            int groupId = Convert.ToInt32(comboBoxGroup.SelectedValue);
            byte? subgroupNumber = null;
            try
            {
                subgroupNumber = Convert.ToByte(textBoxSubgroupNumber.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            short? cardNumber = null;
            try
            {
                cardNumber = Convert.ToInt16(textBoxCardNumber.Text);
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

            return student;
        }

        private StudentAddress GetStudentAddressFromFields()
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

            StudentAddress studentAddress = new StudentAddress
            {
                Region = region,
                City = city,
                Street = street,
                HouseNumber = houseNumber,
                ApartmentNumber = apartmentNumber
            };

            return studentAddress;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Student student = GetStudentFromFields();
            StudentAddress studentAddress = GetStudentAddressFromFields();

            studentService.Add(student, studentAddress);

            StudentsShow();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Student student = GetStudentFromFields();
            student.Id = studentView.Id;
            student.StudentAddressId = studentView.StudentAddressId;

            StudentAddress studentAddress = GetStudentAddressFromFields();
            studentAddress.Id = studentView.StudentAddressId;

            studentService.Update(student, studentAddress);

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
