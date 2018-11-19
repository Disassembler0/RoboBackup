using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RoboBackup {
    /// <summary>
    /// Network credentials for use with UNC paths.
    /// </summary>
    [Serializable]
    public class Credential {
        /// <summary>
        /// Username associated with the credential.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password associated with the credential.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Decrypts <see cref="Credential"/> using AES128-CBC with <see cref="Task.Guid"/> as password for PBKDF2.
        /// </summary>
        /// <param name="guid"><see cref="Task.Guid"/> to be used as PBKDF2 password.</param>
        /// <param name="credential"><see cref="string"/> with encrypted <see cref="Credential"/>.</param>
        /// <returns>Decrypted <see cref="Credential"/>.</returns>
        public static Credential Decrypt(string guid, string credential) {
            if (string.IsNullOrEmpty(credential)) {
                return null;
            }
            byte[] cipherbytes = Convert.FromBase64String(credential);
            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            Array.Copy(cipherbytes, salt, 16);
            Array.Copy(cipherbytes, 16, iv, 0, 16);
            using (AesManaged aes = new AesManaged() { KeySize = 128 } )
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(guid, salt, 1024))
            using (ICryptoTransform decryptor = aes.CreateDecryptor(pbkdf2.GetBytes(16), iv))
            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
            using (BinaryWriter writer = new BinaryWriter(cryptoStream)) {
                writer.Write(cipherbytes, 32, cipherbytes.Length - 32);
                cryptoStream.FlushFinalBlock();
                return Deserialize(Encoding.UTF8.GetString(memoryStream.ToArray()));
            }
        }

        /// <summary>
        /// Encrypts <see cref="Credential"/> using AES128-CBC with <see cref="Task.Guid"/> as password for PBKDF2.
        /// </summary>
        /// <param name="guid"><see cref="Task.Guid"/> to be used as PBKDF2 password.</param>
        /// <param name="credential"><see cref="Credential"/> to be encrypted.</param>
        /// <returns><see cref="string"/> with encrypted <see cref="Credential"/>.</returns>
        /// This is really more obfuscation than encryption as all compounds are in the config, but it's still good enough as a defense against prying eyes.
        /// Security-wise this isn't worse than using native Windows Credential Manager as anyone can read passwords from that one too.
        public static string Encrypt(string guid, Credential credential) {
            if (string.IsNullOrEmpty(credential.Username) && string.IsNullOrEmpty(credential.Password)) {
                return null;
            }
            using (AesManaged aes = new AesManaged() { KeySize = 128 } ) {
                aes.GenerateIV();
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(guid, 16, 1024))
                using (ICryptoTransform encryptor = aes.CreateEncryptor(pbkdf2.GetBytes(16), aes.IV))
                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                using (BinaryWriter writer = new BinaryWriter(cryptoStream)) {
                    writer.Write(Encoding.UTF8.GetBytes(Serialize(credential)));
                    cryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(pbkdf2.Salt.Concat(aes.IV).Concat(memoryStream.ToArray()).ToArray());
                }
            }
        }

        /// <summary>
        /// Deserializes <see cref="string"/> into a <see cref="Credential"/>.
        /// </summary>
        /// <param name="credential"><see cref="string"/> to be deserialized.</param>
        /// <returns>Deserialized <see cref="Credential"/>.</returns>
        private static Credential Deserialize(string credential) {
            string[] split = credential.Split(new char[] { '\n' }, 2);
            return new Credential() { Username = split[0], Password = split[1] };
        }

        /// <summary>
        /// Serializes <see cref="Credential"/> into a <see cref="string"/>.
        /// </summary>
        /// <param name="credential">The <see cref="Credential"/> to be serialized.</param>
        /// <returns>Serialized <see cref="Credential"/> as <see cref="string"/>.</returns>
        private static string Serialize(Credential credential) {
            return string.Format("{0}\n{1}", credential.Username, credential.Password);
        }
    }
}
