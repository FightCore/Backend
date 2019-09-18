using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FightCore.Configuration;
using Microsoft.Extensions.Primitives;

namespace FightCore.Services.Encryption
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts a string using an algorithm.
        /// </summary>
        /// <param name="plainText">The string to be encrypted.</param>
        /// <returns>The encrypted value.</returns>
        string Encrypt(string plainText, string iv);

        /// <summary>
        /// Decrypts the provided encrypted string.
        /// </summary>
        /// <param name="encryptedText">The string the be decrypted.</param>
        /// <returns>The decrypted value.</returns>
        string Decrypt(string encryptedText, string iv);

        /// <summary>
        /// Generates a unique IV.
        /// </summary>
        /// <returns>The generated IV.</returns>
        string GetIV();
    }

    public class AesEncryptionService : IEncryptionService
    {
        private readonly byte[] _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="AesEncryptionService"/> class.
        /// </summary>
        public AesEncryptionService()
        {
            _key = Convert.FromBase64String(ConfigurationBuilder.Configuration.Encryption.Key);
        }

        /// <inheritdoc />
        public string Encrypt(string plainText, string iv)
        {
            byte[] encrypted;
            var ivBytes = Convert.FromBase64String(iv);
            // Create a new AesManaged.    
            using (var aesManaged = new AesManaged())
            {
                var encryptor = aesManaged.CreateEncryptor(_key, ivBytes);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        encrypted = memoryStream.ToArray();
                    }
                }
            }

            // Return encrypted data    
            return Convert.ToBase64String(encrypted);
        }

        /// <inheritdoc />
        public string Decrypt(string encryptedText, string iv)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
            {
                throw new ArgumentException(nameof(encryptedText));
            }

            string plaintext;
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var ivBytes = Convert.FromBase64String(iv);

            using (var aesManaged = new AesManaged())
            {
                var decryptor = aesManaged.CreateDecryptor(_key, ivBytes);
                using (var memoryStream = new MemoryStream(encryptedBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            plaintext = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public string GetIV()
        {
            using (var aesManaged = new AesManaged())
            {
                aesManaged.GenerateIV();
                return Convert.ToBase64String(aesManaged.IV);
            }
        }
    }
}
