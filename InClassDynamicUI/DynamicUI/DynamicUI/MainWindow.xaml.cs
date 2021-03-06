﻿using System;
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

        TextBox txtMessage;

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
            txtMessage = new TextBox();

            AddToStack(txtMessage);

            //Create button and to StackPanel
            var btnGo = new Button() { Content = "Go", Background = Brushes.Red, Margin = new Thickness(0, 25, 0, 0) };
            btnGo.Click += ShowMessage;

            AddToStack(btnGo);
        }

        private void ShowMessage(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(txtMessage.Text);
        }

        private void AddToStack(Control con)
        {
            MiddleStackPanel.Children.Add(con);
        }

    }
}
