using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace CommonUtils.ByteHelper
{
    class ImageToByte
    {
        /// <summary>
        /// 将Image转换为byte[]
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns>byte[]</returns>
        public static byte[] ConvertImage(Image image)
        {
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, (object)image);
            ms.Close();
            return ms.ToArray();
        }
    }
}
