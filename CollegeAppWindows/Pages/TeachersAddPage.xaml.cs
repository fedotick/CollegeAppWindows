using CollegeAppWindows.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CollegeAppWindows.Services;
using CollegeAppWindows.Utilities;
using System.Linq;

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

        private void TextBoxFullName_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxFullName, textBlockFullName, nameof(Teacher.FullName));
        }

        private void ComboBoxCathedra_LostFocus(object sender, RoutedEventArgs e)
        {
            if (comboBoxCathedra.SelectedIndex == 0)

            {
                textBlockCathedra.Text = "Cathedra is required!";
            }
            else
            {
                textBlockCathedra.Text = "";
            }
        }

        private void TextBoxExperience_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxExperience, textBlockExperience, nameof(Teacher.Experience));
        }

        private void TextBoxDateOfBirth_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxDateOfBirth, textBlockDateOfBirth, nameof(Teacher.DateOfBirth));
        }

        private void TextBoxPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxPhoneNumber, textBlockPhoneNumber, nameof(Teacher.PhoneNumber));
        }

        private void TextBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxEmail, textBlockEmail, nameof(Teacher.Email));
        }

        private void TextBoxRegion_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxRegion, textBlockRegion, nameof(TeacherAddress.Region));
        }

        private void TextBoxCity_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxCity, textBlockCity, nameof(TeacherAddress.City));
        }

        private void TextBoxStreet_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxStreet, textBlockStreet, nameof(TeacherAddress.Street));
        }

        private void TextBoxHouseNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxHouseNumber, textBlockHouseNumber, nameof(TeacherAddress.HouseNumber));
        }

        private void TextBoxApartmentNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateTextBox(textBoxApartmentNumber, textBlockApartmentNumber, nameof(TeacherAddress.ApartmentNumber));
        }

        private void ValidateTextBox(TextBox textBox, TextBlock textBlock, string propertyName)
        {
            string? text = textBox.Text != "" ? textBox.Text : null;

            List<string> errors = new List<string>();

            switch (propertyName)
            {
                case nameof(Teacher.FullName):
                    errors = ValidationUtil.ValidateProperty(new Teacher { FullName = text }, propertyName);
                    break;
                case nameof(Teacher.Experience):
                    byte? experience = null;
                    try
                    {
                        if (text != null)
                        {
                            experience = Convert.ToByte(text);
                        }
                            
                        errors = ValidationUtil.ValidateProperty(new Teacher { Experience = experience }, propertyName);
                    }
                    catch
                    {
                        errors.Add("Incorect experience!");
                    }
                    break;
                case nameof(Teacher.DateOfBirth):
                    DateTime? dateOfBirth = null;
                    try
                    {
                        if (text != null)
                        {
                            dateOfBirth = DateTime.ParseExact(text, "dd.MM.yyyy", null);
                        }
                            
                        errors = ValidationUtil.ValidateProperty(new Teacher { DateOfBirth = dateOfBirth }, propertyName);
                    }
                    catch
                    {
                        errors.Add("Incorect date!");
                    }
                    break;
                case nameof(Teacher.PhoneNumber):
                    errors = ValidationUtil.ValidateProperty(new Teacher { PhoneNumber = text }, propertyName);
                    break;
                case nameof(Teacher.Email):
                    errors = ValidationUtil.ValidateProperty(new Teacher { Email = text }, propertyName);
                    break;
                case nameof(TeacherAddress.Region):
                    errors = ValidationUtil.ValidateProperty(new TeacherAddress { Region = text }, propertyName);
                    break;
                case nameof(TeacherAddress.City):
                    errors = ValidationUtil.ValidateProperty(new TeacherAddress { City = text }, propertyName);
                    break;
                case nameof(TeacherAddress.Street):
                    errors = ValidationUtil.ValidateProperty(new TeacherAddress { Street = text }, propertyName);
                    break;
                case nameof(TeacherAddress.HouseNumber):
                    errors = ValidationUtil.ValidateProperty(new TeacherAddress { HouseNumber = text }, propertyName);
                    break;
                case nameof(TeacherAddress.ApartmentNumber):
                    short? apartmentNumber = null;
                    try
                    {
                        if (text != null)
                        {
                            apartmentNumber = Convert.ToInt16(text);
                        }

                        errors = ValidationUtil.ValidateProperty(new TeacherAddress { ApartmentNumber = apartmentNumber }, propertyName);
                    }
                    catch 
                    {
                        errors.Add("Incorect apartment number!");
                    }
                    break;
            }

            if (errors.Any())
            {
                textBlock.Text = string.Join("\n", errors);
            }
            else
            {
                textBlock.Text = "";
            }
        }

        private Teacher GetTeacherFromFields()
        {
            string fullName = textBoxFullName.Text;
            int cathedraId = Convert.ToInt32(comboBoxCathedra.SelectedValue);
            byte? experience = null;
            try
            {
                experience = Convert.ToByte(textBoxExperience.Text);
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
