using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Carneiro.RenamePhotos
{
    class Program
    {
        private static string _path;

        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                {
                    _path = ".";
                }
                else
                {
                    _path = args.First();
                }

                List<string> files = Directory.GetFiles(_path, "*.jpg").ToList();

                Console.WriteLine($"Renaming {files.Count} images.");

                var orderFiles = new Dictionary<string, string>();

                for (int i = 0; i < files.Count; i++)
                {
                    Console.WriteLine($"Getting Date Taken for {files[i]}...");
                    DateTime date = GetDateTakenFromImage(files[i]);
                    orderFiles.Add(files[i], date.ToString("yyyyMMdd-HHmmss"));
                }

                orderFiles = orderFiles.OrderBy(t => t.Value).ToDictionary(t => t.Key, t => t.Value);
                int index = 0;
                foreach (var newFile in orderFiles)
                {
                    string newFileName = $@"{newFile.Value}-{(++index).ToString("D4")}.jpg";
                    Console.WriteLine($"File {newFile}. New Name: {newFileName}");
                    File.Move(newFile.Key, $"{_path}\\{newFileName}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.GetBaseException().Message);
                Console.WriteLine(e.GetBaseException().StackTrace);
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine("Finished. Press return key to close.");
                Console.ReadKey();
            }
        }

        private static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream picStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                JpegBitmapDecoder decoder = new JpegBitmapDecoder(picStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);

                BitmapMetadata metaData = new BitmapMetadata("jpg");
                BitmapFrame frame = BitmapFrame.Create(decoder.Frames[0]);

                metaData = (BitmapMetadata)frame.Metadata;
                DateTime dateTaken = DateTime.Parse(metaData.DateTaken);
                return dateTaken;

            }
        }
    }
}
