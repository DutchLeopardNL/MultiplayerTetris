using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ServerProject.Data
{
	public static class Encrypter
	{
		private static byte[] IV = { 187, 165, 69, 255, 230, 174, 56, 74, 46, 87, 255, 203, 93, 21, 168, 114 };

		/// <summary>
		/// Encrypt text given a key.
		/// </summary>
		/// <param name="plainText"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static byte[] Encrypt(string plainText, string key)
		{
			byte[] encrypted;
			byte[] keyBytes = GetKeyBytes(key);

			using (AesManaged aes = new AesManaged())
			{
				ICryptoTransform encryptor = aes.CreateEncryptor(keyBytes, IV);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter sw = new StreamWriter(cs))
						{
							sw.Write(plainText);
						}
						encrypted = ms.ToArray();
					}
				}
			}
			return encrypted;
		}

		/// <summary>
		/// Decrypt a byte array given a key.
		/// </summary>
		/// <param name="cipherText"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string Decrypt(byte[] cipherText, string key)
		{
			string plaintext = null;
			byte[] keyBytes = GetKeyBytes(key);

			using (AesManaged aes = new AesManaged())
			{
				ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, IV);
				using (MemoryStream ms = new MemoryStream(cipherText))
				{
					using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader reader = new StreamReader(cs))
						{
							plaintext = reader.ReadToEnd();
						}
					}
				}
			}

			return plaintext;
		}

		/// <summary>
		/// Get a 32 byte array which contains a custom key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private static byte[] GetKeyBytes(string key)
		{
			byte[] result = new byte[32];

			int i = 0;
			foreach (byte b in Encoding.ASCII.GetBytes(key))
			{
				result[i++] = b;
			}

			return result;
		}

		/// <summary>
		/// Custom array SubArray method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static T[] SubArray<T>(this T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}
	}
}
