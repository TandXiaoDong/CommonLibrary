using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;


namespace CommonUtils.DBHelper
{
    class DESEncrypt
	{
        #region ========加密======== 
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "litianping");
        }
        
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
#pragma warning disable CS0618 // 类型或成员已过时
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 类型或成员已过时

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] inputByteArray = Encoding.Default.GetBytes(Text);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        #endregion

        #region ========解密======== 
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "litianping");
        }
        
        public static string Decrypt(string strText, string strKey)
        {
            byte[] inputByteArray = new byte[strText.Length / 2];
            for (int x = 0; x < inputByteArray.Length; x++)
            {
                inputByteArray[x] = (byte)Convert.ToInt32(strText.Substring(x * 2, 2), 16);
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
#pragma warning disable CS0618 // 类型或成员已过时
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5").Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 类型或成员已过时

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
    }
}
