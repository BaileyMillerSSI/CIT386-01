using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml;
using System.Xml.Serialization;

namespace ReadWriteXmlAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String XmlPath = @"us-states.xml";
        XmlDocument doc = new XmlDocument();

        public MainWindow()
        {
            InitializeComponent();

            GenerateAutoPopulatedInputFields();
            LoadXmlFileIntoDataGrid();
        }


        private void LoadXmlFileIntoDataGrid()
        {

            


            //To-Do add populate the XML data into the data grid
            var ds = new DataSet("State Data");

            


            ds.ReadXml(XmlPath);

            if (ds.Tables.Count != 0)
            {
                var dv = new DataView(ds.Tables[1]);
                FileDataGrid.ItemsSource = dv;
                FileDataGrid.UpdateLayout();
            }
            else
            {

                FileDataGrid.ItemsSource = null;
                FileDataGrid.Columns.Clear();
                FileDataGrid.UpdateLayout();
            }
        }

        private void DeleteBtnClicked(object sender, RoutedEventArgs e)
        {
            //Find the name of the current state being editted, primary key
            var DataRow = (DataGridRow)FileDataGrid.ItemContainerGenerator.ContainerFromIndex(FileDataGrid.SelectedIndex);

            var dgs = (DataGridCell)(FileDataGrid.Columns[0].GetCellContent(DataRow).Parent);

            var stateName = (dgs.Content as TextBlock).Text;

            doc.Load(XmlPath);

            var nodeToRemove = doc.SelectSingleNode($"//state[@name=\"{stateName}\"]");
            nodeToRemove.ParentNode.RemoveChild(nodeToRemove);

            doc.Save(XmlPath);

            LoadXmlFileIntoDataGrid();
        }

        private void GenerateAutoPopulatedInputFields()
        {
            //To-Do get the fields required to auto generate
            var columnsToGenerate = FileDataGrid.Columns.Where(x=> x is DataGridTextColumn).ToList();

            //Build a new text box for each one
            foreach (var textBox in columnsToGenerate)
            {
                AutoContent.Children.Add(BuildTextBox(textBox.Header.ToString(), textBox.SortMemberPath));
            }

            var addBtn = new Button() { Content = "Add Data", Margin = new Thickness(10, 5, 10, 0) };
            addBtn.Click += AddRecordToData;
            AutoContent.Children.Add(addBtn);
        }

        private void AddRecordToData(object sender, RoutedEventArgs e)
        {
            //Finds all the text boxes that were auto generated
            var nameValue = AutoContent.Children.Cast<TextBox>().Where(x=>(x.Tag as TextBoxProperties).PathName == "name").FirstOrDefault();
            var capitalValue = AutoContent.Children.Cast<TextBox>().Where(x => (x.Tag as TextBoxProperties).PathName == "capital").FirstOrDefault();
            var mostPopValue = AutoContent.Children.Cast<TextBox>().Where(x => (x.Tag as TextBoxProperties).PathName == "most-populous-city").FirstOrDefault();

            //Loads the doc data
            doc.Load(XmlPath);
            
            //Creates a node that is going to be added to the XML data
            var newNode = doc.CreateNode(XmlNodeType.Element, "state", "");

            //Creates and sets name attribute
            var nameAttribte = doc.CreateAttribute("name");
            nameAttribte.Value = nameValue.Text;

            //Creates and sets capital attribute
            var capitalAttribte = doc.CreateAttribute("capital");
            capitalAttribte.Value = capitalValue.Text;

            //Creates and sets most populous city attribute
            var mostPopAttribute = doc.CreateAttribute("most-populous-city");
            mostPopAttribute.Value = mostPopValue.Text;

            //Adds attributes to node
            newNode.Attributes.Append(nameAttribte);
            newNode.Attributes.Append(capitalAttribte);
            newNode.Attributes.Append(mostPopAttribute);

            //Adds node to document
            doc.DocumentElement.AppendChild(newNode);

            //Saves document
            doc.Save(XmlPath);

            //Freshes the view
            LoadXmlFileIntoDataGrid();

        }

        private TextBox BuildTextBox(string placeHolder, string pathName)
        {
            var tb = new TextBox() { Text = placeHolder, Tag = new TextBoxProperties() { PathName = pathName, PlaceHolder = placeHolder}, Margin = new Thickness(10, 5, 10, 0) };
            tb.GotKeyboardFocus +=AddGotKeyboardFocus ;
            tb.LostFocus += AddLostFocus;
            return tb;
        }

        private void SaveChanges(string columnPath, string newValue)
        {
            //Find the name of the current state being editted, primary key
            var DataRow = (DataGridRow)FileDataGrid.ItemContainerGenerator.ContainerFromIndex(FileDataGrid.SelectedIndex);

            var dgs = (DataGridCell)(FileDataGrid.Columns[0].GetCellContent(DataRow).Parent);

            var stateName = (dgs.Content as TextBlock).Text;

            doc.Load(XmlPath);


            var nodeToUpdate = doc.SelectSingleNode($"//state[@name=\"{stateName}\"]");

            nodeToUpdate.Attributes[columnPath].Value = newValue;

            doc.Save(XmlPath);

            LoadXmlFileIntoDataGrid();
        }

        private void FileDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            SaveChanges(e.Column.SortMemberPath, (e.EditingElement as TextBox).Text);
        }

        

        #region Helpers for textboxs
        private void AddGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var tagData = ((sender as TextBox).Tag as TextBoxProperties);
            if ((sender as TextBox).Text == tagData.PlaceHolder)
            {
                (sender as TextBox).Text = "";
            }
        }

        private void AddLostFocus(object sender, RoutedEventArgs e)
        {
            var tagData = ((sender as TextBox).Tag as TextBoxProperties);
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = tagData.PlaceHolder;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RootGrid.Focus();
        }
        #endregion
    }

    //Comes in handy because I needed to pass some data along with each text box, the tag property is helpful for this
    public class TextBoxProperties
    {
        public string PathName;
        public string PlaceHolder;
    }
}
