#region → Usings   .
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 05.02.12     M.Whaba           Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eSourceApp.Common
{
    /// <summary>
    /// Hashing for eSource Query String.
    /// </summary>
    public static class Hashing
    {
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Encrypts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="IV">The IV.</param>
        /// <param name="dataToEncrypt">The data to encrypt.</param>
        /// <returns></returns>
        public static string Encrypt(byte[] key,byte[] IV, string dataToEncrypt)
        {
            // Initialise
            AesManaged encryptor = new AesManaged();
            
            // Set the key
            encryptor.KeySize = 256;
            encryptor.Key = key;
            encryptor.IV = IV;

            // create a memory stream
            using (MemoryStream encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    byte[] utfD1 = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();

                    // Return the encrypted data
                    return Convert.ToBase64String(encryptionStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="IV">The IV.</param>
        /// <param name="encryptedString">The encrypted string.</param>
        /// <returns></returns>
        public static string Decrypt(byte[] key,byte[] IV, string encryptedString)
        {
            // Initialise
            AesManaged decryptor = new AesManaged();
            byte[] encryptedData = Convert.FromBase64String(encryptedString);

            // Set the key
            decryptor.Key = key;
            decryptor.IV = IV;

            // create a memory stream
            using (MemoryStream decryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    decrypt.Write(encryptedData, 0, encryptedData.Length);
                    decrypt.Flush();
                    decrypt.Close();

                    // Return the unencrypted data
                    byte[] decryptedData = decryptionStream.ToArray();
                    return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
                }
            }
        }

        #endregion

        #endregion
    }
}
