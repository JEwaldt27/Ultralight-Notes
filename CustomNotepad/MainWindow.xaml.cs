using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Documents;

namespace CustomNotepad
{
    public partial class MainWindow : Window
    {
        private string currentFilePath = string.Empty;
        private bool isDarkMode = false;

        public MainWindow()
        {
            InitializeComponent();
            FontSizeDropdown.SelectionChanged += FontSizeDropdown_SelectionChanged;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                TxtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                currentFilePath = openFileDialog.FileName;
                Title = $"Custom Notepad - {Path.GetFileName(currentFilePath)}";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAs();
            }
            else
            {
                File.WriteAllText(currentFilePath, TxtEditor.Text);
            }
        }

        private void BtnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, TxtEditor.Text);
                currentFilePath = saveFileDialog.FileName;
                Title = $"Ultralight Notepad - {Path.GetFileName(currentFilePath)}";
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtEditor.Clear();
        }

        private void BtnToggleDarkMode_Click(object sender, RoutedEventArgs e)
        {
            if (!isDarkMode)
            {
                Background = Brushes.Black;
                TxtEditor.Background = Brushes.Black;
                TxtEditor.Foreground = Brushes.White;
                TxtEditor.CaretBrush = Brushes.White;
                isDarkMode = true;
            }
            else
            {
                Background = Brushes.White;
                TxtEditor.Background = Brushes.White;
                TxtEditor.Foreground = Brushes.Black;
                TxtEditor.CaretBrush = Brushes.Black;
                isDarkMode = false;
            }
        }

        private void FontSizeDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeDropdown.SelectedItem != null)
            {
                TxtEditor.FontSize = Convert.ToDouble((FontSizeDropdown.SelectedItem as ComboBoxItem).Content);
            }
        }
    }
}
