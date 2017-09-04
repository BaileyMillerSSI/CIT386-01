using CalculatorApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        CalculatorViewModel vm = new CalculatorViewModel();



        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        public async void OnCalculatorBtnClick(object sender, RoutedEventArgs e)
        {
            var senderText = (sender as Button).Content.ToString();
            //Do the same thing as a key press for that button
            if (senderText == "+" || senderText == "-" || senderText == "/" || senderText == "*")
            {
                if (vm.CanAppendOperator())
                {
                    vm.DisplayText = senderText;
                }
            }
            else {
                vm.DisplayText = senderText;
            }
        }

        public void ClearButtonClicked(object sender, RoutedEventArgs e)
        {
            vm.Clear();
        }

        public void BackupButtonClicked(object sender, RoutedEventArgs e)
        {
            vm.Backup();
        }

        public  async void FunctionButtonClicked(object sender, RoutedEventArgs e)
        {
            var functionOperator = (sender as Button).Content.ToString();
            if (functionOperator == "=")
            {
                var answer = await SolveProblemAsync();
                vm.SetAnswer(answer);
            }
            else {
                if (vm.CanAppendOperator())
                {
                    vm.DisplayText = functionOperator;
                }
            }
            
        }

        private async void ListenForKeyPresses(object sender, KeyEventArgs e)
        {
            //To-Do listen for any number key up events
            // Any symbol up events, +, -, /, *, (=, Enter)

            switch (e.Key)
            {
                case Key.Add:
                    if (vm.CanAppendOperator())
                    {
                        vm.DisplayText = "+";
                    }
                    break;
                case Key.Subtract:
                    if (vm.CanAppendOperator())
                    {
                        vm.DisplayText = "-";
                    }
                    break;
                case Key.Multiply:
                    if (vm.CanAppendOperator())
                    {
                        vm.DisplayText = "*";
                    }
                    break;
                case Key.Divide:
                    if (vm.CanAppendOperator())
                    {
                        vm.DisplayText = "/";
                    }
                    break;
                case Key.OemPlus:
                    var _answer1 = await SolveProblemAsync();
                    vm.SetAnswer(_answer1);
                    break;
                case Key.Enter:
                    var answer = await SolveProblemAsync();
                    vm.SetAnswer(answer);
                    break;

                //Now for the numbers
                case Key.NumPad1:
                    vm.DisplayText = "1";
                    break;
                case Key.NumPad2:
                    vm.DisplayText = "2";
                    break;
                case Key.NumPad3:
                    vm.DisplayText = "3";
                    break;
                case Key.NumPad4:
                    vm.DisplayText = "4";
                    break;
                case Key.NumPad5:
                    vm.DisplayText = "5";
                    break;
                case Key.NumPad6:
                    vm.DisplayText = "6";
                    break;
                case Key.NumPad7:
                    vm.DisplayText = "7";
                    break;
                case Key.NumPad8:
                    vm.DisplayText = "8";
                    break;
                case Key.NumPad9:
                    vm.DisplayText = "9";
                    break;
                case Key.NumPad0:
                    vm.DisplayText = "0";
                    break;
                case Key.Decimal:
                    if (vm.CanAppendDecimal())
                    {
                        vm.DisplayText = ".";
                    }
                    break;
                //Functional helpers
                //Clear simple backspace
                case Key.Escape:
                    //This should clear the text
                    vm.Clear();
                    break;
                case Key.Delete:
                    vm.Backup();
                    break;

                case Key.Back:
                    vm.Backup();
                    break;

                default:
                    break;
            }

        }


        public async Task<float> SolveProblemAsync()
        {
            return await await Task.Factory.StartNew(async () =>
            {
                try
                {
                    var clean = await vm.ClearProblemOfSpaces();
                    if (!clean.EndsWith("/0"))
                    {
                        var solver = new DataTable();
                        var didSolve = float.TryParse(solver.Compute(clean, "").ToString(), out float sol);
                        if (didSolve)
                        {
                            return sol;
                        }
                        else
                        {
                            return 0f;
                        }
                    }
                    else
                    {
                        return 0f;
                    }
                }
                catch (Exception)
                {
                    return 0f;
                }
            });
        }
    }
}
