using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace CommonUtils.ByteHelper
{
    class ByteToImage
    {
        /// <summary>
        /// 将byte[]转换为Image
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>Image</returns>
        public static Image ReadImage(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            ms.Close();
            return (Image)new BinaryFormatter().Deserialize(ms);
        }

        /// <summary>
        /// 字节数组生成图片
        /// </summary>
        /// <param name="Bytes">字节数组</param>
        /// <returns>图片</returns>
        public static Image byteArrayToImage(byte[] Bytes)
        {
            MemoryStream ms = new MemoryStream(Bytes);
            return Image.FromStream(ms, true);
        }
    }
}
