using System.Security.Cryptography;
using System.Text;

namespace SafeShareAPI.Provider
{
    public static class EncryptionProvider
    {
        public static string MD5Hash(string clearText)
        {
            using MD5 md5 = MD5.Create(); return Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(clearText)));
        }
        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(ConfigProvider.EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream memoryStream = new();
                using (CryptoStream cryptoStream = new(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(clearBytes, 0, clearBytes.Length); cryptoStream.Close();
                }
                clearText = Convert.ToBase64String(memoryStream.ToArray());
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(ConfigProvider.EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream memoryStream = new();
                using (CryptoStream cryptoStream = new(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(cipherBytes, 0, cipherBytes.Length); cryptoStream.Close();
                }
                cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
            }
            return cipherText;
        }
        public static string StringToHex(string input)
        {
            byte[] stringBytes = Encoding.Unicode.GetBytes(input);
            StringBuilder sbBytes = new(stringBytes.Length * 2);
            foreach (byte b in stringBytes) { sbBytes.AppendFormat("{0:X2}", b); }
            return sbBytes.ToString();
        }
        public static string HexToString(string hexInput)
        {
            int numberChars = hexInput.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2) { bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16); }
            return Encoding.Unicode.GetString(bytes);
        }
    }
}
