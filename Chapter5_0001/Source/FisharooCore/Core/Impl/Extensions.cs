using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Impl
{
    public static class Extensions
    {
        public static string Encrypt(this string s, string key)
        {
            return Cryptography.Encrypt(s, key);
        }

        public static string Decrypt(this string s, string key)
        {
            return Cryptography.Decrypt(s, key);
        }

        //CHAPTER 4
        public static string TimestampToString(this System.Data.Linq.Binary binary)
        {
            byte[] binarybytes = binary.ToArray();
            string result = "";
            foreach (byte b in binarybytes)
            {
                result += b.ToString() + "|";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        //CHAPTER 4
        public static System.Data.Linq.Binary StringToTimestamp(this string s)
        {
            string[] arr = s.Split('|');
            byte[] bytes = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                bytes[i] = Convert.ToByte(arr[i]);
            }
            return bytes;
        }

        //CHAPTER 4
public static string ToMD5Hash(this string s)
{
    return Cryptography.CreateMD5Hash(s);   
}
    }
}
