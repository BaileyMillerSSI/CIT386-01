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
using System.Xml;

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

            if (ds.Tables.Count != 0)
            {
                var dv = new DataView(ds.Tables[0]);
                BookDataGrid.ItemsSource = dv;
            }
            else {
                BookDataGrid.Columns.Clear();
                BookDataGrid.ItemsSource = null;
            }

            
        }

        private void DeleteBtnClicked(object sender, RoutedEventArgs e)
        {
            var dg = BookDataGrid;
            var dr = (DataGridRow)dg.ItemContainerGenerator
                                        .ContainerFromIndex(dg.SelectedIndex);
            var dgs = (DataGridCell)dg.Columns[0].GetCellContent(dr).Parent;

            var bookId = Convert.ToInt32((dgs.Content as TextBlock).Text);

            var doc = new XmlDocument();
            doc.Load(xmlPath);

            var nodeToRemove = doc.SelectSingleNode($"//Book[@id=\"{bookId}\"]");
            nodeToRemove.ParentNode.RemoveChild(nodeToRemove);

            doc.Save(xmlPath);

            BindDataToGrid();
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Time to save some things

        }
    }
}
