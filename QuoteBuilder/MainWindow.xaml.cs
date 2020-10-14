using System;
using System.IO;
using System.Text;
using System.Windows;

namespace QuoteBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string text = File.ReadAllText("template.rtf");

            text = text.Replace("[RecipientName]", recipientNameTextBox.Text);
            text = text.Replace("[Date]", DateTime.Now.Date.ToLongDateString());
            text = text.Replace("[Address]", BuildAddress());

            string filename = ($"{DateTime.Now.Date.ToString("dd-MM-yyyy")}-{recipientNameTextBox.Text}.rtf").Replace(" ", string.Empty);

            File.WriteAllText(filename, text);

            if (File.Exists(filename))
            {
                System.Diagnostics.Process.Start(@"C:\Program Files\Windows NT\Accessories\wordpad.exe", filename);
            }

            Application.Current.Shutdown();
        }

        private string BuildAddress()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"\pard\sl240\slmult1 ");
            AppendIfNotNull(sb, recipientNameTextBox.Text.Trim());
            AppendIfNotNull(sb, AddressLine1TextBox.Text.Trim());
            AppendIfNotNull(sb, TownTextBox.Text.Trim());
            AppendIfNotNull(sb, CountyTextBox.Text.Trim());
            AppendIfNotNull(sb, PostcodeTextBox.Text.Trim());
            return sb.ToString();
        }

        private void AppendIfNotNull(StringBuilder sb, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                sb.AppendLine(@"" + text + @"\par");
            }
        }
    }
}