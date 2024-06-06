using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Utility
{
    public class PasswordHashing
    {
        public string Hasher(string password)
        {
            try
            {
                byte[] encData_byte = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public string VerifyPassword(string password)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
