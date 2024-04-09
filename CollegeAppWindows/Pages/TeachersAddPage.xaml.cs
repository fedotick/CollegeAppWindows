using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using CollegeAppWindows.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using CollegeAppWindows.Utilities; 
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CollegeAppWindows.Services;
using System.Data.SqlClient;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Логика взаимодействия для TeachersAddPage.xaml
    /// </summary>
    public partial class TeachersAddPage : Page
    {
        private Repository<Cathedra> cathedraRepository;
        private TeacherService teacherService;

        public TeachersAddPage()
        {
            InitializeComponent();

            DataBase dataBase = new DataBase();
            SqlConnection connection = dataBase.GetConnection();
            
            cathedraRepository = new Repository<Cathedra>(connection);
            teacherService = new TeacherService(connection);

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
            Teacher teacher = new Teacher();

            try
            {
                teacher = new Teacher
                {
                    FullName = textBoxFullName.Text,
                    CathedraId = Convert.ToInt32(comboBoxCathedra.SelectedValue),
                    Experience = Convert.ToInt32(textBoxExperience.Text),
                    DateOfBirth = DateTime.ParseExact(textBoxDateOfBirth.Text, "dd.MM.yyyy", null),
                    PhoneNumber = textBoxPhoneNumber.Text,
                    Email = textBoxEmail.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            TeacherAddress teacherAddress = new TeacherAddress();

            try
            {
                teacherAddress = new TeacherAddress
                {
                    Region = textBoxRegion.Text,
                    City = textBoxCity.Text,
                    Street = textBoxStreet.Text,
                    HouseNumber = textBoxHouseNumber.Text,
                    ApartmentNumber = Convert.ToInt32(textBoxApartmentNumber.Text)
                };
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
