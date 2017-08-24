using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace ReadWriteXaml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileStream DataStream;
        XmlDocument doc;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            Closing += MainWindow_Closing;
            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataStream != null)
            {
                DataStream.Flush();
                DataStream.Close();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFriendXml();
        }

        private void LoadFriendXml()
        {
            var FriendPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "XamlFile.xml");
            DataStream = new FileStream(FriendPath, FileMode.Open, FileAccess.ReadWrite);

            doc = new XmlDocument();
            doc.Load(DataStream);

            var root = doc.DocumentElement;
            var subRoot = (XmlElement)root.FirstChild;

            var children = subRoot.ChildNodes;

            for (int i = 0; i < children.Count; i++)
            {
                Stack.Children.Add(CreateLabel(children[i].Name));
                Stack.Children.Add(CreateTextBox());
            }
            var btnAdd = new Button() { Content = "Add Friend" };
            btnAdd.Click += AddFriend;
            Stack.Children.Add(btnAdd);
        }

        private void AddFriend(object sender, RoutedEventArgs e)
        {

            var root = doc.DocumentElement;
            var subRoot = (XmlElement)root.FirstChild;

            var newFriend = doc.CreateElement(subRoot.Name);

            //Loop through stack panel

            foreach (var child in Stack.Children)
            {
                var lblChild = String.Empty;

                if (child is Label)
                {
                    //Get the Content value
                    lblChild = (child as Label).Content.ToString();

                    var propertyElemet = doc.CreateElement(lblChild);
                    newFriend.AppendChild(propertyElemet);
                }
                else if (child is TextBox)
                {
                    //Get the Text value
                }

            }

            subRoot.AppendChild(newFriend);

            doc.Save(DataStream);


            

            

        }

        private Label CreateLabel(string cont)
        {
            var lblChild = new Label() { Content=cont};
            return lblChild;
        }

        private TextBox CreateTextBox()
        {
            return new TextBox();
        }
    }
}
