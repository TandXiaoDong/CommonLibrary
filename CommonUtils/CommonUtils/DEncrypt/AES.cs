using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;

public static class AES
{
    /// <summary>
    /// 默认密钥向量
    /// </summary>
    private static byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };

    /// <summary>
    /// 3次加密（得出正确解）
    /// </summary>
    /// <param name="aStrString"></param>
    /// <param name="aStrKey"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static string Encrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.CBC)
    {
        try
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(aStrKey),
                Mode = mode
            };
            if (mode == CipherMode.CBC)
            {
                des.IV = iv;
            }
            var desEncrypt = des.CreateEncryptor();
            byte[] buffer = Encoding.UTF8.GetBytes(aStrString);
            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 3次解密（得出正确解）
    /// </summary>
    /// <param name="aStrString"></param>
    /// <param name="aStrKey"></param>
    /// <param name="mode"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string TDecrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.CBC)
    {
        try
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(aStrKey),
                Mode = mode,
                Padding = PaddingMode.PKCS7
            };
            if (mode == CipherMode.CBC)
            {
                des.IV = iv;
            }
            var desDecrypt = des.CreateDecryptor();
            var result = "";
            byte[] buffer = Convert.FromBase64String(aStrString);
            result = Encoding.UTF8.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            return result;
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }


}
