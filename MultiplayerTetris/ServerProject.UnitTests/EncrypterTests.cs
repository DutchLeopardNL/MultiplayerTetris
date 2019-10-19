using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerProject.Data;

namespace ServerProject.UnitTests
{
	[TestClass]
	public class EncrypterTests
	{
		[TestMethod]
		public void EncryptDecrypt_ReturnsEqual()
		{
			string sentence = "Once upon a time..";
			string key = "password123";

			byte[] encrypted = Encrypter.Encrypt(sentence, key);
			string decrypted = Encrypter.Decrypt(encrypted, key);

			Assert.AreEqual(sentence, decrypted);
		}
	}
}
