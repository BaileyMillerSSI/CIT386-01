using System;
using System.Collections.Generic;
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

namespace DynamicUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GenerateUIStuff();
        }

        private void GenerateUIStuff()
        {
            //Create label and add to StackPanel
            var lblMessage = new Label() { //Short hand property setting
                Content = "Message"
            };

            AddToStack(lblMessage);

            //Create textBox and add to StackPanel
            var txtMessage = new TextBox();

            AddToStack(txtMessage);

            //Create button and to StackPanel
            var btnGo = new Button() { Content = "Go" };

            AddToStack(btnGo);

        }


        private void AddToStack(Control con)
        {
            MiddleStackPanel.Children.Add(con);
        }

    }
}
