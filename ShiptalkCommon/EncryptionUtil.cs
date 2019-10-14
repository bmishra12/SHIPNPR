using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;


namespace ShiptalkCommon
{
    internal sealed class EncryptionUtil
    {
        //24 byte or 192 bit key and IV for TripleDES
        //private static byte[] KEY_192 = { 42, 16, 93, 156, 78, 4, 218, 32, 15, 167,
        //44, 80, 26, 250, 155, 112, 2, 94, 11, 204,
        //119, 35, 184, 197 };

        //private static byte[] IV_192 = { 55, 103, 246, 79, 36, 99, 167, 3, 42, 5,
        //62, 83, 184, 7, 209, 13, 145, 23, 200, 58,
        //173, 10, 121, 222 };
        private static byte[] KEY_192 = { 22, 1, 3, 16, 178, 224, 112, 35, 111, 111,
                                            221, 55, 46, 222, 77, 83, 13, 13, 210, 233, 99, 200, 10, 11 };

        private static byte[] IV_192 = { 83, 112, 98, 193, 33, 2, 157, 64, 24, 57, 214,
                                            129, 92, 49, 52, 145, 27, 71, 54, 235, 77, 122, 65, 167, 5 };

        private static byte[] KEY_64 = { 10, 15, 81, 11, 20, 81, 10, 4 };
        private static byte[] IV_64 = { 43, 32, 57, 93, 15, 99, 167, 3 };

        //TRIPLE DES decryption
        public static string EncryptTripleDES(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);

                sw.Write(value);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                //convert back to a string
                return Convert.ToBase64String(ms.GetBuffer());
            }
            else
                return string.Empty;
        }


        //TRIPLE DES decryption
        public static string DecryptTripleDES(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();

                //convert from string to byte array
                byte[] buffer = Convert.FromBase64String(value);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);

                return sr.ReadToEnd();
            }
            else
                return string.Empty;
        }

        //Regular DES decrypt. For Triple DES refer to DecryptTripleDES
        //public static string DESDecrypt(string value)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        //        byte[] buffer = Convert.FromBase64String(source);
        //        MemoryStream ms = new MemoryStream(buffer);
        //        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(key64, iv64), CryptoStreamMode.Read);
        //        StreamReader sr = new StreamReader(cs);

        //        return sr.ReadToEnd();
        //    }
        //    return "";
        //}

        //Regular DES Encrypt. For Triple DES refer to EncryptTripleDES
        public static string EncryptDES(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);

                sw.Write(value);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            }
            return "";
        }


    }
}
