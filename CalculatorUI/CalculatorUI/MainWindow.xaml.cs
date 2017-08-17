using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CalculatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string _DisplayText { get; set; } = "|";

        public String DisplayText
        {
            get
            {
                return _DisplayText;
            }
            private set
            {
                _DisplayText = value;
                OnPropertyChanged("DisplayText");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnCalculatorBtnClick(object sender, RoutedEventArgs e)
        {

            if (TextIsWaitCursor())
            {
                ClearTextArea();
            }

            DisplayText += (sender as Button).Content as string; 
        }

        private void ListenForKeyPresses()
        {
            //To-Do listen for any number key up events
            // Any symbol up events, +, -, /, *, (=, Enter)
        }

        private void NotifyTextChange()
        {
            OnPropertyChanged("DisplayText");
        }

        private Boolean TextIsWaitCursor()
        {
            return DisplayText == "|";
        }

        private void ClearTextArea()
        {
            DisplayText = String.Empty;
        }

    }
}
