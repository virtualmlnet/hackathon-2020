using System.Drawing;
using System.IO;

namespace SmartLabeling.API.Helpers
{
    public static class ImageHelper
    {
        public static Image GetImageFromBytes(this byte[] bytes)
        {
            if ((bytes == null) || (bytes.Length == 0)) return null;
            using var stream = new MemoryStream(bytes);
            var result = Image.FromStream(stream);
            
            return result;
        }
    }
}
