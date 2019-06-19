using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Services.Log;
using Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public static class EncryptionExtensions
    {
        private const int IV_LENGTH = 12;
        private const int TAG_LENGTH = 16;

        //Cache informations crypto
        private static IDictionary<AesEncryptionInfoCodes, IAesEncryptionInfo> _crytoInfos = new Dictionary<AesEncryptionInfoCodes, IAesEncryptionInfo>();
        private static ILogService _logger = new LogService(true);

        [LogConfig(NoLog = true)]
        //Get information crypto from database
        public static IAesEncryptionInfo GetCryptoInfo(AesEncryptionInfoCodes code = AesEncryptionInfoCodes.SYSTEM)
        {
            if (!_crytoInfos.ContainsKey(code))
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PlataformaRio2CConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetCryptInfoByCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@code", (int)code);
                        con.Open();
                        var rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            rd.Read();
                            var cryptoInfo = new AppAesEncryptionInfo
                            {
                                Password = rd.GetString(1),
                                Salt = rd.GetString(2),
                                PasswordIterations = rd.GetInt32(3),
                                InitialVector = rd.GetString(4),
                                KeySize = rd.GetInt32(5)
                            };
                            _crytoInfos.Add(code, cryptoInfo);
                            rd.Close();
                        }
                        else
                        {
                            rd.Close();
                            throw new InvalidDataException("Impossível obter a chave de criptografia do banco de dado");
                        }
                    }
                }
            }
            return _crytoInfos[code];
        }

        [LogConfig(NoLog = true)]
        public static string AesEncrypt(this string planText, AesEncryptionInfoCodes code = AesEncryptionInfoCodes.SYSTEM)
        {
            var cryptoInfo = GetCryptoInfo(code);
            if (cryptoInfo == null) throw new System.NullReferenceException("Não foi possivel obter chaves de criptografia do banco");
            return planText.CgmEncrypt(cryptoInfo);
        }

        [LogConfig(NoLog = true)]
        public static string AesDecrypt(this string encryptedText, AesEncryptionInfoCodes code = AesEncryptionInfoCodes.SYSTEM)
        {
            var cryptoInfo = GetCryptoInfo(code);
            if (cryptoInfo == null) throw new System.NullReferenceException("Não foi possivel obter chaves de criptografia do banco");
            return encryptedText.CgmDecrypt(cryptoInfo);
        }

        [LogConfig(NoLog = true)]
        public static string AesEncrypt(this string planText, string salt, AesEncryptionInfoCodes code = AesEncryptionInfoCodes.SYSTEM)
        {
            var cryptoInfo = GetCryptoInfo(code);
            if (cryptoInfo == null) throw new System.NullReferenceException("Não foi possivel obter chaves de criptografia do banco");
            var newCryptoInfo = new AppAesEncryptionInfo(cryptoInfo);
            newCryptoInfo.Salt = salt;
            return planText.CgmEncrypt(newCryptoInfo);
        }

        [LogConfig(NoLog = true)]
        public static string AesDecrypt(this string encryptedText, string salt, AesEncryptionInfoCodes code = AesEncryptionInfoCodes.SYSTEM)
        {
            var cryptoInfo = GetCryptoInfo(code);
            if (cryptoInfo == null) throw new System.NullReferenceException("Não foi possivel obter chaves de criptografia do banco");
            var newCryptoInfo = new AppAesEncryptionInfo(cryptoInfo);
            newCryptoInfo.Salt = salt;
            return encryptedText.CgmDecrypt(newCryptoInfo);
        }

        [LogConfig(NoLog = true)]
        public static string CgmEncrypt(this string plainText, IAesEncryptionInfo aesEncryptionInfo)
        {
            return plainText.AesEncryptCgm(aesEncryptionInfo.Password, aesEncryptionInfo.Salt,
                                        aesEncryptionInfo.PasswordIterations, aesEncryptionInfo.InitialVector,
                                        aesEncryptionInfo.KeySize);
        }

        [LogConfig(NoLog = true)]
        public static string CbcEncrypt(this string plainText, IAesEncryptionInfo aesEncryptionInfo)
        {
            return plainText.AesEncrypt(aesEncryptionInfo.Password, aesEncryptionInfo.Salt,
                                        aesEncryptionInfo.PasswordIterations, aesEncryptionInfo.InitialVector,
                                        aesEncryptionInfo.KeySize);
        }

        [LogConfig(NoLog = true)]
        public static string CgmDecrypt(this string cipherText, IAesEncryptionInfo aesEncryptionInfo)
        {
            return cipherText.AesDecryptCgm(aesEncryptionInfo.Password, aesEncryptionInfo.Salt,
                                         aesEncryptionInfo.PasswordIterations, aesEncryptionInfo.InitialVector,
                                         aesEncryptionInfo.KeySize);
        }

        [LogConfig(NoLog = true)]
        public static string CbcDecrypt(this string cipherText, IAesEncryptionInfo aesEncryptionInfo)
        {
            return cipherText.AesDecrypt(aesEncryptionInfo.Password, aesEncryptionInfo.Salt,
                                         aesEncryptionInfo.PasswordIterations, aesEncryptionInfo.InitialVector,
                                         aesEncryptionInfo.KeySize);
        }

        #region Encrypt and Decrypt true methods

        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <param name="password">Password to encrypt with</param>
        /// <param name="salt">Salt to encrypt with</param>
        /// <param name="passwordIterations">Number of iterations to do</param>
        /// <param name="initialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="keySize">Can be 128, 192, or 256</param>
        /// <returns>An encrypted string</returns>
        public static string AesEncrypt(this string plainText, string password, string salt, int passwordIterations, string initialVector, int keySize)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return "";
            }

            var initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(salt);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var derivedPassword = new Rfc2898DeriveBytes(password, saltValueBytes, passwordIterations);

            var keyBytes = derivedPassword.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };

            byte[] cipherTextBytes;

            using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        cipherTextBytes = memoryStream.ToArray();

                        memoryStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();

            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="cipherText">Text to be decrypted</param>
        /// <param name="password">Password to decrypt with</param>
        /// <param name="salt">Salt to decrypt with</param>
        /// <param name="passwordIterations">Number of iterations to do</param>
        /// <param name="initialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="keySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public static string AesDecrypt(this string cipherText, string password, string salt, int passwordIterations, string initialVector, int keySize)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return "";
            }

            var initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(salt);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var derivedPassword = new Rfc2898DeriveBytes(password, saltValueBytes, passwordIterations);

            var keyBytes = derivedPassword.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };

            var plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount;

            using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
            {
                using (var memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                        memoryStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();

            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }

        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <param name="password">Password to encrypt with</param>
        /// <param name="salt">Salt to encrypt with</param>
        /// <param name="passwordIterations">Number of iterations to do</param>
        /// <param name="initialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="keySize">Can be 128, 192, or 256</param>
        /// <returns>An encrypted string</returns>
        public static string AesEncryptCgm(this string plainText, string password, string salt, int passwordIterations, string initialVector, int keySize)
        {
            if (string.IsNullOrEmpty(plainText)) return "";

            using (AuthenticatedAesCng aes = new AuthenticatedAesCng())
            {
                var initialVectorBytes = Encoding.UTF8.GetBytes(initialVector);
                var saltValueBytes = Encoding.UTF8.GetBytes(salt);
                var derivedPassword = new Rfc2898DeriveBytes(password, saltValueBytes, passwordIterations);
                var keyBytes = derivedPassword.GetBytes(keySize / 8);
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                aes.Key = keyBytes;
                aes.IV = GenerateIV();
                aes.CngMode = CngChainingMode.Gcm;
                aes.AuthenticatedData = initialVectorBytes;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (IAuthenticatedCryptoTransform encryptor = aes.CreateAuthenticatedEncryptor())
                    {
                        using (CryptoStream cs = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            // Write through and retrieve encrypted data.
                            cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cs.FlushFinalBlock();
                            byte[] cipherTextBytes = memoryStream.ToArray();

                            // Retrieve tag and create array to hold encrypted data.
                            byte[] authenticationTag = encryptor.GetTag();
                            byte[] encrypted = new byte[cipherTextBytes.Length + aes.IV.Length + authenticationTag.Length];

                            // Set needed data in byte array.
                            aes.IV.CopyTo(encrypted, 0);
                            authenticationTag.CopyTo(encrypted, IV_LENGTH);
                            cipherTextBytes.CopyTo(encrypted, IV_LENGTH + TAG_LENGTH);

                            // Store encrypted value in base 64.
                            return Convert.ToBase64String(encrypted);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="cipherText">Text to be decrypted</param>
        /// <param name="password">Password to decrypt with</param>
        /// <param name="salt">Salt to decrypt with</param>
        /// <param name="passwordIterations">Number of iterations to do</param>
        /// <param name="initialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="keySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public static string AesDecryptCgm(this string cipherText, string password, string salt, int passwordIterations, string initialVector, int keySize)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";

            using (AuthenticatedAesCng aes = new AuthenticatedAesCng())
            {
                var initialVectorBytes = Encoding.UTF8.GetBytes(initialVector);
                var saltValueBytes = Encoding.UTF8.GetBytes(salt);
                var derivedPassword = new Rfc2898DeriveBytes(password, saltValueBytes, passwordIterations);
                var keyBytes = derivedPassword.GetBytes(keySize / 8);
                var cipherTextBytes = Convert.FromBase64String(cipherText);

                aes.Key = keyBytes;
                aes.IV = GetIV(cipherTextBytes);
                aes.Tag = GetTag(cipherTextBytes);
                cipherTextBytes = RemoveTagAndIV(cipherTextBytes);
                aes.CngMode = CngChainingMode.Gcm;
                aes.AuthenticatedData = initialVectorBytes;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                        {

                            //Decrypt through stream.
                            cryptoStream.Write(cipherTextBytes, 0, cipherTextBytes.Length);
                            cryptoStream.FlushFinalBlock();

                            // Remove from stream and convert to string.
                            byte[] decrypted = memoryStream.ToArray();
                            return Encoding.UTF8.GetString(decrypted);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generate hash
        /// </summary>
        /// <param name="plainText">Text to be generate</param>
        /// <param name="type">Sha512 implementations (optional)</param>
        /// <param name="salt">Byte[] Salt to encrypt with (optional)</param>
        /// <returns></returns>
        public static string GenerateHash(this string plainText, SHA512TYPES type = SHA512TYPES.SHA512Cng, byte[] salt = null)
        {
            if (string.IsNullOrEmpty(plainText)) return "";

            byte[] saltValueBytes = null;
            if (salt == null)
            {
                saltValueBytes = new byte[24];
                using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
                {
                    provider.GetNonZeroBytes(saltValueBytes);
                }
            }
            else
            {
                saltValueBytes = salt;
            }

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltValueBytes.Length];

            plainTextBytes.CopyTo(plainTextWithSaltBytes, 0);
            saltValueBytes.CopyTo(plainTextWithSaltBytes, plainTextBytes.Length);

            using (HashAlgorithm provider = GetHashSha512(type))
            {
                var hashBytes = provider.ComputeHash(plainTextWithSaltBytes);
                var hashWithSaltBytes = new byte[hashBytes.Length + saltValueBytes.Length];
                hashBytes.CopyTo(hashWithSaltBytes, 0);
                saltValueBytes.CopyTo(hashWithSaltBytes, hashBytes.Length);

                return Convert.ToBase64String(hashWithSaltBytes);
            }
        }

        /// <summary>
        /// Verify hash
        /// </summary>
        /// <param name="hashValue">Hash to be compare</param>
        /// <param name="plainText">Text to be generate hash and compare with hash</param>
        /// <param name="type">Sha512 implementations (optional)</param>
        /// <returns></returns>
        public static bool VerifyHash(this string hashValue, string plainText, SHA512TYPES type = SHA512TYPES.SHA512Cng)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBytes = 512 / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString = plainText.GenerateHash(type, saltBytes);

            return hashValue == expectedHashString;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static byte[] StringHexToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string StringPlanToStringHex(string plan)
        {
            byte[] ba = Encoding.Default.GetBytes(plan);
            return ByteArrayToString(ba);
        }

        #endregion

        #region Private Methods

        // generateIV - generates a random 12 byte IV.
        // Pre: none.
        // Post: returns the random nonce.
        private static byte[] GenerateIV()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] nonce = new byte[IV_LENGTH];
                rng.GetBytes(nonce);
                return nonce;
            }
        }

        // getTag - parses authentication tag from the ciphertext.
        // Pre: passed the byte array.
        // Post: returns the tag as a byte array.
        [LogConfig(NoLog = true)]
        private static byte[] GetTag(byte[] arr)
        {
            byte[] tag = new byte[TAG_LENGTH];
            Array.Copy(arr, IV_LENGTH, tag, 0, TAG_LENGTH);
            return tag;
        }

        // getIV - parses IV from ciphertext.
        // Pre: Passed the ciphertext byte array.
        // Post: Returns byte array containing the IV.
        [LogConfig(NoLog = true)]
        private static byte[] GetIV(byte[] arr)
        {
            byte[] IV = new byte[IV_LENGTH];
            Array.Copy(arr, 0, IV, 0, IV_LENGTH);
            return IV;
        }

        // removeTagAndIV - removes the tag and IV from the byte array so it may be decrypted.
        // Pre: Passed the ciphertext byte array.
        // Post: Peturns a byte array consisting of only encrypted data.
        [LogConfig(NoLog = true)]
        private static byte[] RemoveTagAndIV(byte[] arr)
        {
            byte[] enc = new byte[arr.Length - TAG_LENGTH - IV_LENGTH];
            Array.Copy(arr, IV_LENGTH + TAG_LENGTH, enc, 0, arr.Length - IV_LENGTH - TAG_LENGTH);
            return enc;
        }

        private static HashAlgorithm GetHashSha512(SHA512TYPES type)
        {
            switch (type)
            {
                case SHA512TYPES.SHA512Cng:
                    {
                        return new SHA512Cng();
                    }
                case SHA512TYPES.SHA512Managed:
                    {
                        return new SHA512Managed();
                    }
                case SHA512TYPES.SHA512CryptoServiceProvider:
                    {
                        return new SHA512CryptoServiceProvider();
                    }
                default:
                    {
                        return new SHA512Cng();
                    }
            }
        }

        #endregion
    }

    public enum SHA512TYPES
    {
        SHA512Cng = 0,
        SHA512Managed = 1,
        SHA512CryptoServiceProvider = 2
    }

    public enum AesEncryptionInfoCodes
    {
        SYSTEM = 0,
        FILE_LOG = 1,
        API = 2
    }
}
