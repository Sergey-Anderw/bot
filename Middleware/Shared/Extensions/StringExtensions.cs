using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Shared.Helpers;

namespace Shared.Extensions
{
    public static class StringExtensions
    {
        public static string Clean(this string input)
        {
            if (input == null)
                return null!;

            if (!string.IsNullOrEmpty(input))
                return input.Trim();

            return input;
        }

        public static string CleanWithNotNumeric(this string input)
        {
	        input = Clean(input);
	        return input != null ? Regex.Replace(Clean(input), @"[^0-9]+", "") : "";
		}

        public static DateTime AsDateTime(this string input)
        {
            if (DateTime.TryParseExact(input, Constants.DateTimeStringFormat, Constants.DefaultCultureInfo, DateTimeStyles.AssumeLocal, out var result1))
                return result1;
            if (DateTime.TryParseExact(input, "MM/dd/yyyy HH:mm:ss", Constants.DefaultCultureInfo, DateTimeStyles.AssumeLocal, out var result2))
                return result2;

            return DateTime.ParseExact(input, Constants.DateTimeStringFormat, Constants.DefaultCultureInfo);
        }
        public static string ToUrlPath(this string input)
        {
            if(input  == null)
                return string.Empty;
            var splitChar = '\\';
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                splitChar = Path.DirectorySeparatorChar;
            var array = input.Split(new char[] { splitChar }, StringSplitOptions.RemoveEmptyEntries) ?? new string[] { };
            return string.Join('/', array);
        }

        public static string RemoveStart(this string value, string removeString)
        {
            if (value.EndsWith(removeString))
            {
                return value.Substring(removeString.Length, value.Length - removeString.Length);
            }
            return value;
        }
        public static string RemoveEnd(this string value, string removeString)
        {
            if (value.EndsWith(removeString))
            {
                return value.Substring(0, value.Length - removeString.Length);
            }
            return value;
        }

        public static string GetLastChankFromPath(this string value)
        {
            var array = value.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);
            if(array.Length == 0)
                return value;
            return array.Last();
        }
        public static T DeserializeAs<T>(this string s)
        {
            return JsonConvertHelper.DeserializeObject<T>(s);
        }

        public static string Encrypt(this string text, string encryptionKey, string salt = null!)
        {
            var saltBytes = Encoding.Unicode.GetBytes(salt);
            var dataBytes = Encoding.Unicode.GetBytes(text);

            using var hmac = new HMACSHA256(saltBytes);
            using var encrypter = Aes.Create();
            var keyLength = encrypter.Key.Length;
            encrypter.Key = GetKey(encryptionKey, saltBytes, keyLength);

            var iv = encrypter.IV;

            byte[] encData;
            using (var ms = new MemoryStream())
            {
                var cryptoTransform = encrypter.CreateEncryptor();
                using (var hmacStream = new CryptoStream(ms, hmac, CryptoStreamMode.Write))
                using (var cs = new CryptoStream(hmacStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cs.Write(dataBytes, 0, dataBytes.Length);
                    cs.FlushFinalBlock();
                }

                encData = ms.ToArray();
            }

            using (var stream = new MemoryStream())
            {
                stream.Write(iv, 0, iv.Length);
                stream.Write(hmac.Hash!, 0, hmac.Hash!.Length);
                stream.Write(encData, 0, encData.Length);
                stream.Flush();

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static string Decrypt(this string cipherText, string encryptionKey, string salt = null!)
        {
            var saltBytes = Encoding.Unicode.GetBytes(salt);

            byte[] incomingBytes = Convert.FromBase64String(cipherText);
            using (var encrypter = Aes.Create())
            {
                var keyLength = encrypter.Key.Length;
                var key = GetKey(encryptionKey, saltBytes, keyLength);
                encrypter.Key = key;

                using (var hmac = new HMACSHA256(saltBytes))
                {
                    var hashBytes = new byte[hmac.HashSize / 8];
                    using (var incomingStream = new MemoryStream(incomingBytes))
                    {
                        var length = encrypter.IV.Length;
                        var encryptorViBytes = new byte[length];
                        incomingStream.Read(encryptorViBytes, 0, length);
                        incomingStream.Read(hashBytes, 0, hashBytes.Length);

                        encrypter.IV = encryptorViBytes;

                        using (var decryptedStream = new MemoryStream())
                        {
                            var cryptoTransform = encrypter.CreateDecryptor();
                            using (var cs = new CryptoStream(decryptedStream, cryptoTransform, CryptoStreamMode.Write))
                            using (var hmacStream = new CryptoStream(cs, hmac, CryptoStreamMode.Write))
                            {
                                incomingStream.CopyTo(hmacStream);
                                hmacStream.FlushFinalBlock();

                                if (!hashBytes.SequenceEqual(hmac.Hash!))
                                    throw new InvalidDataException();
                            }

                            var decryptedBytes = decryptedStream.ToArray();
                            cipherText = Encoding.Unicode.GetString(decryptedBytes);
                        }
                    }
                }
            }

            return cipherText;
        }

        public static string[] SliceChain(this string source)
        {
            if(string.IsNullOrEmpty(source))
                return new string[] { string.Empty };
            source = source.Replace("\\", "/");
            return source.Split('/', StringSplitOptions.RemoveEmptyEntries);
        }

        [Obsolete("Obsolete")]
        private static byte[] GetKey(string encryptionKey, byte[] saltBytes, int keyLength)
        {
            byte[] key;
            using var pdb = new Rfc2898DeriveBytes(encryptionKey, saltBytes);
            key = pdb.GetBytes(keyLength);

            return key;
        }
    }
}