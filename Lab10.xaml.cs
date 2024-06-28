using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab10_11
{
    /// <summary>
    /// Логика взаимодействия для Lab10.xaml
    /// </summary>
    public partial class Lab10 : Page
    {
        private string fileName;

        public Lab10()
        {
            InitializeComponent();
            fileName = string.Empty;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files(*.txt)|*.txt";
            if (ofd.ShowDialog() == true)
            {
                string fileContent = await File.ReadAllTextAsync(ofd.FileName);
                string[] components = fileContent.Split(new char[] { ' ', ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                DateTime[] dates = components.Select(component => DateTime.Parse(component)).ToArray();
                DateTime latestDate = dates.Max();

                pathText.Text = ofd.FileName;
                FileContentTextBox.Text = fileContent;
                Count.Text = latestDate.ToString("yyyy-MM-dd");
            }
        }
    }
}
