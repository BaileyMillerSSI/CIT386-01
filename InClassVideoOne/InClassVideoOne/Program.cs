using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace InClassVideoOne
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Valks!");
            Console.WriteLine("Easily save your favorite videos from YouTube.");

            using (var service = Client.For(YouTube.Default))
            {
                while (true)
                {
                    Console.Write("Enter your video's ID: ");

                    string id = Console.ReadLine();

                    while (String.IsNullOrEmpty(id))
                    {
                        id = Console.ReadLine();
                    }

                    Console.WriteLine("Awesome! Downloading...");

                    var video = service.GetVideo("https://youtube.com/watch?v=" + id);

                    Console.Write("Finished! Would you like to save the video to Downloads? [y/n] ");

                    char opt = Console.ReadKey().KeyChar;

                    Console.WriteLine();

                    string folder;

                    if (char.ToUpper(opt) == 'Y')
                        folder = GetDefaultFolder();
                    else
                    {
                        Console.Write("Please tell us where you'd like to save it: ");
                        folder = Console.ReadLine();
                    }

                    string path = Path.Combine(folder, video.FullName);

                    Console.WriteLine("Saving...");
                    
                    File.WriteAllBytes(path, video.GetBytes());

                    Console.WriteLine("Done.");
                }
            }
        }

        static string GetDefaultFolder()
        {
            var home = Environment.GetFolderPath(
                Environment.SpecialFolder.MyVideos);

            return home;
        }
    }
}
