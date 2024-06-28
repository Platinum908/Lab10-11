using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab10_11
{
    public partial class Lab11 : Page
    {
        private const string FilePath = "numbers.bin";

        public Lab11()
        {
            InitializeComponent();
        }

        private void ProcessNumbers(object sender, RoutedEventArgs e)
        {
            try
            {
                // Считываем числа из TextBox
                string[] inputNumbers = InputTextBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int[] numbers = inputNumbers.Select(int.Parse).ToArray();

                // Записываем числа в бинарный файл
                WriteNumbersToFile(numbers);
                string beforeText = "Числа до изменения: " + String.Join(", ", numbers);
                BeforeChangeText.Text = beforeText;

                // Читаем и удваиваем числа из файла
                int[] modifiedNumbers = ReadAndModifyNumbers();
                string afterText = "Числа после изменения: " + String.Join(", ", modifiedNumbers);
                AfterChangeText.Text = afterText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WriteNumbersToFile(int[] numbers)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
            {
                foreach (int number in numbers)
                {
                    writer.Write(number);
                }
            }
        }

        private int[] ReadAndModifyNumbers()
        {
            List<int> numbers = new List<int>();

            using (BinaryReader reader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    numbers.Add(reader.ReadInt32());
                }
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] *= 2;
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
            {
                foreach (int number in numbers)
                {
                    writer.Write(number);
                }
            }

            return numbers.ToArray();
        }
    }
}


