using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace CodeValue.ScrumBoard.Client.Common
{
    public static class Utils
    {
       

        public static byte[] ImageToBytes(string path)
        {
            return File.ReadAllBytes(path);           
        }

        public static BitmapImage BytesToImage(byte[] bytes)
        {           
            using (var stream = new MemoryStream(bytes))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                
                image.EndInit();
                return image;
            }


        }

      
    }
}
