using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Main.WebHelper
{
    public class LoginAcc
    {
        public static string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(bytes);

                // Chuyển mảng byte sang chuỗi hex
                StringBuilder sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // dạng hex, 2 chữ số
                }
                return sb.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput.Equals(storedHashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}