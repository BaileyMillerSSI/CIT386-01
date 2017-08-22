using System.Windows;

namespace PersonalInfo
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

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var fn = FirstNameTxt.Text.Trim();
            var ln = LastNameTxt.Text.Trim();

            MessageBox.Show($"Hello {fn} {ln}", "Hello User", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
