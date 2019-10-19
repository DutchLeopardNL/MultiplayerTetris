using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServerProject.Data
{
	public class FileIO
	{
		/// <summary>
		/// Read a file.
		/// If the file does not exist, it is being created.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="file"></param>
		/// <returns></returns>
		public static Dictionary<string, int> Read(string path, string file)
		{
			try
			{
				using (Stream stream = File.Open($"{path}/{file}", FileMode.Open))
				{
					var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					return (Dictionary<string, int>)binaryFormatter.Deserialize(stream);
				}
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(path);
				return Read(path, file);
			}
			catch (FileNotFoundException)
			{
				using (File.Create($"{path}/{file}")) { }
				return new Dictionary<string, int>();
			}
			catch (SerializationException)
			{
				return new Dictionary<string, int>();
			}
		}


		/// <summary>
		/// Write to a file.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="file"></param>
		/// <param name="objectToWrite"></param>
		public static void Write(string path, string file, Dictionary<string, int> objectToWrite)
		{
			using (Stream stream = File.Open($"{path}/{file}", FileMode.Create))
			{
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				binaryFormatter.Serialize(stream, objectToWrite);
			}
		}

	}
}
