using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace CodeValue.ScrumBoard.Client.Common
{
    public static class Utils
    {
        public static byte[] ImageToBytes(BitmapImage image)
        {
            using (var stream = image.StreamSource)
            {
                Byte[] buffer = null;
                if (stream != null && stream.Length > 0)
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        buffer = br.ReadBytes((Int32)stream.Length);
                    }
                }
                return buffer;
            }           
        }

        public static BitmapImage BytesToImage(byte[] bytes)
        {            
            using (var stream = new MemoryStream(bytes))
            { 
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }            
        }

        public static BitmapImage ImageFromPath(string path)
        {
            try
            {
                var uri = new Uri(path);
                return new BitmapImage(uri);
            }
            catch
            {
                return null;
            }
        }
    }
}
