using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Strokes.Clear();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var saveTask = SaveAsPngAsync();    
        }


        private async Task SaveAsPngAsync()
        {
            var canvasAtSaveState = XamlWriter.Save(DrawingCanvas);
            var w = DrawingCanvas.ActualWidth;
            var h = DrawingCanvas.ActualHeight;
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            await Task.Factory.StartNew(() => {
                try
                {
                    InkCanvas cc = XamlReader.Parse(canvasAtSaveState) as InkCanvas;
                    var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                    using (var fs = new FileStream(System.IO.Path.Combine(desktopPath, "picture.png"), FileMode.Create, FileAccess.ReadWrite))
                    {
                        RenderTargetBitmap rtb = new RenderTargetBitmap((int)w, (int)h, 96d, 96d, PixelFormats.Default);
                        rtb.Render(cc);
                        BitmapEncoder pngEncoder = new PngBitmapEncoder();
                        pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
                        pngEncoder.Save(fs);
                    }
                }
                catch (Exception ex)
                {

                }
            }, cts.Token, TaskCreationOptions.PreferFairness, scheduler);
        }
    }
}
