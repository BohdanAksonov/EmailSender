using EmailSender.Application.Interfaces;
using EmailSender.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace EmailSender.Infrastructure.Services
{
    public class AesService : IAesService
    {
        private readonly AesConfiguration _aesConfiguration;

        public AesService(IOptions<AesConfiguration> options)
        {
            _aesConfiguration = options.Value;
        }

        public string DecryptStringFromBytes(string encryptedValue)
        {
            var keybytes = Encoding.UTF8.GetBytes(_aesConfiguration.Key);
            var iv = Encoding.UTF8.GetBytes(_aesConfiguration.Iv);
            var cipherText = Convert.FromBase64String(encryptedValue);

            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (keybytes == null || keybytes.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext = null;

            using Aes aesAlg = Aes.Create();

            aesAlg.Key = keybytes;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(cipherText);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);

            plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
    }
}
