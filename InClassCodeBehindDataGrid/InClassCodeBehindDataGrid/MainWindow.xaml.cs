using System;
using System.Collections.Generic;
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

namespace InClassCodeBehindDataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        String xmlPath = @"BooksXml.xml";


        public MainWindow()
        {
            InitializeComponent();

            BindDataToGrid();

        }


        private void BindDataToGrid()
        {
            var ds = new DataSet("Books");

            ds.ReadXml(xmlPath);

            var dv = new DataView(ds.Tables[0]);

            BookDataGrid.ItemsSource = dv;
        }
    }
}
