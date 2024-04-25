using CollegeAppWindows.Models;
using CollegeAppWindows.Services;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CollegeAppWindows.Reports
{
    /// <summary>
    /// Логика взаимодействия для TeachersReport.xaml
    /// </summary>
    public partial class TeachersReport : Window
    {
        public TeachersReport()
        {
            InitializeComponent();

            

            // Установка источника данных для отчета RDLC
            CollegeDataSet collegeDataSet = new CollegeDataSet();
            MyReportViewer.ProcessingMode = ProcessingMode.Local;
            MyReportViewer.LocalReport.ReportPath = @"C:\Users\Фёдор Ткачук\OneDrive\CEITI\College\CollegeAppWindows\CollegeAppWindows\Reports\TeachersReport.rdlc";
            MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet_TeacherView", collegeDataSet.Tables[0])); // Предполагая, что ваша таблица находится в первом индексе
            MyReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            MyReportViewer.RefreshReport();
        }
    }
}
