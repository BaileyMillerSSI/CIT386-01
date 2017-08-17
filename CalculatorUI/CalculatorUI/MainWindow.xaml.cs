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

        private void ListenForKeyPresses(object sender, KeyEventArgs e)
        {
            //To-Do listen for any number key up events
            // Any symbol up events, +, -, /, *, (=, Enter)

            switch (e.Key)
            {
                case Key.Add:
                    DisplayText += "+";
                    break;
                case Key.Subtract:
                    DisplayText += "-";
                    break;
                case Key.Multiply:
                    DisplayText += "*";
                    break;
                case Key.Divide:
                    DisplayText += "/";
                    break;
                case Key.OemPlus | Key.Enter:
                    //This is the equal key
                    break;

                //Now for the numbers
                case Key.NumPad1:
                    break;
                case Key.NumPad2:
                    break;
                case Key.NumPad3:
                    break;
                case Key.NumPad4:
                    break;
                case Key.NumPad5:
                    break;
                case Key.NumPad6:
                    break;
                case Key.NumPad7:
                    break;
                case Key.NumPad8:
                    break;
                case Key.NumPad9:
                    break;
                case Key.NumPad0:
                    break;

                //Functional helpers
                //Clear simple backspace
                case Key.Escape:
                    //This should clear the text
                    DisplayText = String.Empty;
                    break;
                case Key.Delete | Key.Back:
                    //Remove the last entered character
                    break;

                default:
                    break;
            }

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
