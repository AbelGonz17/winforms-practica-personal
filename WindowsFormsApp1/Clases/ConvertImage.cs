using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

using System.IO;

namespace WindowsFormsApp1
{
    public class ConvertImage
    {
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }

        public static byte[] ImageToByArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms,ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
