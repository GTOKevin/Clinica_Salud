using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Web_Cita_Medicas.Security
{
    public static class EncryptMD5
    {
        public static string Encrypt(string cadena)
        {
            string hash = "hospitalCibertec";
            byte[] data = UTF8Encoding.UTF8.GetBytes(cadena);


            //Protocolo MD5
            MD5 mD5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();


            tripleDES.Key = mD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;


            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string msjEn)
        {
            string hash = "hospitalCibertec";
            byte[] data = Convert.FromBase64String(msjEn);


            //Protocolo MD5
            MD5 mD5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();


            tripleDES.Key = mD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;


            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}