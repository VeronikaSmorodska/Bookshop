using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BookshopBLL.Infrastructure
{
    public class HashData
    {
        public static string GenerateSHA512(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString());
            }
            return result.ToString();
        }
    }
}
