using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paint
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
            //They're never going to even see this it happens way to fast.
            SaveBtn.Content = "Saving";
            SaveAsPng();
            SaveBtn.Content = "Save";
        }


        private void SaveAsPng()
        {
            var canvasAtSaveState = DrawingCanvas;
            canvasAtSaveState.Background = Background; //Windows background or remove to leave transparent or default
            var w = DrawingCanvas.ActualWidth;
            var h = DrawingCanvas.ActualHeight;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)canvasAtSaveState.ActualWidth, (int)canvasAtSaveState.ActualHeight, 96d, 96d, PixelFormats.Default);
            rtb.Render(canvasAtSaveState);
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
            
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (var fs = new FileStream(Path.Combine(desktopPath, $"{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Year} {DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}.png"), FileMode.Create, FileAccess.ReadWrite))
            {
                pngEncoder.Save(fs);
            }
        }
    }
}
