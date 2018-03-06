using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MarginEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String TITLE = "Margin Editor (ASE Assignment Project)";
        public MainWindow()
        {
            InitializeComponent();
            leftMarginBox.ItemsSource = new List<Double>() { 0, 1, 2, 3, 4, 5 };
            leftMarginBox.SelectedIndex = 0;
            rightMarginBox.ItemsSource = new List<Double>() { 0, 1, 2, 3, 4, 5 };
            rightMarginBox.SelectedIndex = 0;
            rtbEditor.Document = new FlowDocument();

    
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text File Format (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Text);
                this.Title = openFileDialog.FileName + "  -  " + TITLE;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File Format (*.txt)|*.txt";

            if(saveFileDialog.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Text);
                fileStream.Close();
            }
        }

        private void leftMarginBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(leftMarginBox.SelectedItem != null){
                foreach ( Paragraph paragraph in rtbEditor.Document.Blocks)
                {
                    paragraph.Margin = new Thickness( 10 * Convert.ToDouble(leftMarginBox.SelectedValue), 0, 10 * Convert.ToDouble(rightMarginBox.SelectedValue), 0);
                }
            }
        }

        private void rightMarginBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rightMarginBox.SelectedItem != null)
            {
                foreach (Paragraph paragraph in rtbEditor.Document.Blocks)
                {
                    paragraph.Margin = new Thickness(10 * Convert.ToDouble(leftMarginBox.SelectedValue), 0, 10 * Convert.ToDouble(rightMarginBox.SelectedValue), 0);
                }
                
            }
        }
    }
}
