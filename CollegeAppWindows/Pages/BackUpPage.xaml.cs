using CollegeAppWindows.Utilities;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CollegeAppWindows.Pages
{
    /// <summary>
    /// Логика взаимодействия для BackUpPage.xaml
    /// </summary>
    public partial class BackUpPage : Page
    {
        public BackUpPage()
        {
            InitializeComponent();

            btnLoadBackUp.Click += BtnLoadBackUp_Click; ;
            btnSaveBackUp.Click += BtnSaveBackUp_Click; ;
        }

        private void BtnLoadBackUp_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;

                if (SqlUtil.LoadBackUp(path))
                {
                    MessageBox.Show("The backup was loaded successfully!");
                }
                else
                {
                    MessageBox.Show("Error loading backup!");
                }
            }
        }

        private void BtnSaveBackUp_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;

                if (!path.EndsWith(".bak"))
                {
                    path += ".bak";
                }

                if (SqlUtil.SaveBackUp(path))
                {
                    MessageBox.Show("The backup was saved to the following directory: " + path);
                }
                else
                {
                    MessageBox.Show("Error saving backup!");
                }
            }
        }

        //private void создатьBackupToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (DialogResult.OK == saveFileDialog.ShowDialog())
        //    {
        //        string path = saveFileDialog.FileName;
        //        if (!path.EndsWith(".bak")) path += ".bak";
        //        bool result = _base.CreateBackUp(path);
        //        if (result)
        //            MessageBox.Show("Бэкаб был сохраненн в следующую директорию: " + path);
        //        else
        //            MessageBox.Show("Ошибка создания бэкапа!");
        //    }
        //}

        //private void загрузитьBackupToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (DialogResult.OK == openFileDialog.ShowDialog())
        //    {
        //        string path = openFileDialog.FileName;
        //        bool result = _base.LoadBackUp(path);
        //        if (result)
        //            MessageBox.Show("Бэкап был загружен успешно!");
        //        else
        //            MessageBox.Show("Ошибка загрузки бэкапа!");
        //    }
        //}
    }
}
