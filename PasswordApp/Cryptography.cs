using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PasswordApp
{
    public class Cryptography
    {
        private static readonly byte[] Salt = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };

        public static string Encrypt(string plainText, byte[] encryptionKeyBytes)
        {
            byte[] iv = new byte[16]; // Generate a random IV for each encryption
            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKeyBytes;
                aes.GenerateIV(); // Generate a random IV
                iv = aes.IV; // Store the IV for use in decryption

                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(iv, 0, iv.Length); // Prepend IV to the ciphertext
                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (var streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }
                        }
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, byte[] encryptionKeyBytes)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            byte[] iv = new byte[16];
            Array.Copy(buffer, 0, iv, 0, iv.Length); // Extract the IV from the start of the buffer

            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKeyBytes;
                aes.IV = iv;

                using (var memoryStream = new MemoryStream(buffer, iv.Length, buffer.Length - iv.Length))
                {
                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        public static byte[] CreateKey(string password, int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, Salt, Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
    }
}