using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Server.Commons
{
    /// <summary>
    /// Crypto class
    /// </summary>
    public class Hasher
    {
        public static string GetHash(string input)
        {
            input = input ?? "";
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public static bool VerifyHash(string input, string hash)
        {
            string hashOfInput = GetHash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
