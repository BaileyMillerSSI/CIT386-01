using System;
using System.Collections.Generic;
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
using VideoLibrary;

namespace YoutubeVideoDownloader
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var rawData = VideoSearch.Text;
            string searchData = "";
            if (IsInFullLinkForm(rawData))
            {
                searchData = ExtractId(rawData);
            }
            else {
                searchData = rawData.Trim();
            }
            SearchButton.Content = "Downloading";
            var status = await DownloadAndSaveVideo(searchData);
            if (!status)
            {
                MessageBox.Show("Downloading encountered error");
            }
            SearchButton.Content = "Search";
            VideoSearch.Text = "Video Url or Id";

        }

        private string GetDefaultFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }

        private async Task<bool> DownloadAndSaveVideo(string id)
        {
            return await Task.Factory.StartNew(()=> {
                try
                {
                    using (var service = Client.For(YouTube.Default))
                    {
                        var video = service.GetVideo("https://youtube.com/watch?v=" + id);
                        var folder = GetDefaultFolder();

                        if (!video.IsEncrypted)
                        {
                            string path = System.IO.Path.Combine(folder, video.FullName);
                            if (!File.Exists(path))
                            {
                                File.WriteAllBytes(path, video.GetBytes());
                            }
                            else {
                                File.Move(path, path + ".old");
                                File.WriteAllBytes(path, video.GetBytes());
                            }

                            Dispatcher.Invoke(new Action(() => {
                                AfterVideo.Source = new Uri(path);
                                AfterVideo.Play();
                            }), System.Windows.Threading.DispatcherPriority.Normal);
                        }

                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        private string ExtractId(string url)
        {
            var collection = url.ToCharArray();
            //Need to extract the id 
            var startingPoint = url.IndexOf("v=")+2;
            var id = url.Substring(startingPoint, url.Length - startingPoint);
            return id;
        }

        private bool IsInFullLinkForm(string url)
        {
            if (url.Contains("youtube.com"))
            {
                return true;
            }
            else {
                return false;
            }
        }

        private void VideoSearch_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "Video Url or Id")
            {
                (sender as TextBox).Text = "";
            }
        }

        private void VideoSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = "Video Url or Id";
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RootGrid.Focus();
        }
    }
}
